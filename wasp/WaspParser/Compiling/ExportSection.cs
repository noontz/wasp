using System;
using System.Collections.Generic;
using System.Text;
using wasp.enums;
using wasp.Global;
using wasp.Intermediate;

namespace wasp.Compiling
{
    class ExportSection : ISection
    {
        readonly List<byte> exports = new List<byte>();

        public ModuleSections ID => ModuleSections.Export;

        int counter = -1;

        public void AddFunction(Function function, int index)
        {
            counter++;
            exports.AddRange(Leb128.VarUint32(function.Signature.Identifier.Value.Length));
            exports.AddRange(function.Signature.Identifier.Value.Bytes);
            exports.Add((byte) ExternalKinds.Function);
            exports.AddRange(Leb128.VarUint32(index));
        }

        public IEnumerable<byte> Compile()
        {
            yield return (byte)ID;

            var numberOfFunctions = Leb128.VarUint32(counter);

            foreach (var b in Leb128.VarUint32(numberOfFunctions.Length + exports.Count))
                yield return b;

            foreach (var b in Leb128.VarUint32(numberOfFunctions.Length))
                yield return b;

            foreach (var b in exports)
                yield return b;
        }
    }
}
