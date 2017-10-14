using System.Collections.Generic;
using wasp.enums;
using wasp.Global;

namespace wasp.Compiling
{
    class CodeSection : ISection
    {
        readonly List<byte> codeBodies = new List<byte>();

        int indexCount;

        public ModuleSections ID => ModuleSections.Code;

        public IEnumerable<byte> Compile()
        {
            yield return (byte) ID;

            var numberOfBodies = Leb128.VarUint32(indexCount);

            foreach (var b in Leb128.VarUint32(numberOfBodies.Length + codeBodies.Count))
                yield return b;

            foreach (var b in Leb128.VarUint32(numberOfBodies.Length))
                yield return b;

            foreach (var b in codeBodies)
                yield return b;
        }

        public void AddCodeBody(byte[] codeBody, int currentIndex)
        {
            indexCount = currentIndex;
            codeBodies.AddRange(codeBody);
        }
    }
}