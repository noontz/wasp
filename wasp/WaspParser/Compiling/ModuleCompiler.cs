using System.Collections.Generic;

namespace wasp.Compiling
{
    class ModuleCompiler
    {
        readonly byte[] magic = {0x00, 0x61, 0x73, 0x6D};

        readonly byte[] version = {0x01, 0x00, 0x00, 0x00};

        public byte[] CompileWasm()
        {
            var output = new List<byte>(magic);
            output.AddRange(version);
            return output.ToArray();
        }
    }
}