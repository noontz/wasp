using System.IO;
using wasp.Compiling;

namespace wasp
{
    static class API
    {
        public static void Compile(string wasp, string wasm)
        {
            var moduleCompiler = new ModuleCompiler();
            File.WriteAllBytes(wasm, moduleCompiler.CompileWasm());
        }
    }
}