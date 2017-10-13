using System.Collections.Generic;
using System.IO;
using System.Linq;
using wasp.enums;
using wasp.Parsing;
using wasp.Tokenization;

namespace wasp.Compiling
{
    abstract class Section
    {
        protected Section(ModuleSections id) => ID = id;

        public ModuleSections ID { get; }

        public static void Compile(string wasp, string wasm, string javaScript)
        {
           
        }

        public abstract IEnumerable<byte> Compile();
    }
}