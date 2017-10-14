using System;
using System.Collections.Generic;
using System.Linq;
using wasp.enums;
using wasp.Global;
using wasp.Intermediate;

namespace wasp.Compiling
{
    class TypeSection : ISection
    {
        readonly List<TypeBytes> types = new List<TypeBytes>();

        int typesByteCount;

        public ModuleSections ID => ModuleSections.Type;

        public void AddSignature(Signature signature)
        {
            var typeBytes = new TypeBytes(signature);
            var match = types.FirstOrDefault(t => t.Equals(typeBytes));
            if (match == null)
            {
                types.Add(typeBytes);
                typesByteCount += typeBytes.Bytes.Length;
                signature.SetIndex(types.Count - 1);
            }
            else
            {
                signature.SetIndex(types.IndexOf(match));
            }
        }

        public IEnumerable<byte> Compile()
        {
            yield return (byte) ID;

            var numberOfTypes = Leb128.VarUint32(types.Count);
            foreach (var b in Leb128.VarUint32(typesByteCount + numberOfTypes.Length))
                yield return b;

            foreach (var b in numberOfTypes)
                yield return b;

            foreach (var typeBytes in types)
                foreach (var b in typeBytes.Bytes)
                    yield return b;
        }
    }

    class TypeBytes : IEquatable<TypeBytes>
    {
        public TypeBytes(Signature signature)
        {
            var parametersCount = Leb128.VarUint32(signature.Parameters.Count);
            var returnType = signature.ReturnType.Group == TokenGroups.ValueType;
            var length = 3 + parametersCount.Length + (returnType ? 2 : 1);
            Bytes = new byte[length];
            Bytes[0] = Leb128.VarInt7((sbyte) LanguageTypes.Func);
            Buffer.BlockCopy(parametersCount, 0, Bytes, 1, parametersCount.Length);
            for (var i = parametersCount.Length; i < signature.Parameters.Count + parametersCount.Length; i++)
                Bytes[i + 1] = ValueTypeMap.GetCode(signature.Parameters[i - parametersCount.Length].InputType);
            if (returnType)
            {
                Bytes[Bytes.Length - 2] = 0x01;
                Bytes[Bytes.Length - 1] = ValueTypeMap.GetCode(signature.ReturnType);
            }
            else
            {
                Bytes[Bytes.Length - 1] = 0x00;
            }
        }

        public byte[] Bytes { get; }

        public bool Equals(TypeBytes other) => Bytes.SequenceEqual(other.Bytes);

        public override int GetHashCode() => BitConverter.ToInt32(Bytes, 0);
    }
}