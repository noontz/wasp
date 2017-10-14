using System.Collections.Generic;
using wasp.enums;
using wasp.Global;
using wasp.Intermediate;

namespace wasp.Compiling
{
    class FunctionSection : ISection
    {
        public int CurrentIndex { get; private set; } = -1;

        readonly List<byte> typeCodeMap = new List<byte>();

        public ModuleSections ID => ModuleSections.Function;

        public IEnumerable<byte> Compile()
        {
            yield return (byte) ID;

            var numberOfFunctions = Leb128.VarUint32(CurrentIndex);

            foreach (var b in Leb128.VarUint32(numberOfFunctions.Length + typeCodeMap.Count))
                yield return b;

            foreach (var b in Leb128.VarUint32(numberOfFunctions.Length))
                yield return b;

            foreach (var b in typeCodeMap)
                yield return b;
        }

        public void AddFunction(Function function)
        {
            CurrentIndex++;
            typeCodeMap.AddRange(function.Signature.TypeIndex);
        }
    }
}