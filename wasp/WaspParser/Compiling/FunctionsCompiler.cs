using System.Collections.Generic;
using wasp.Intermediate;
using wasp.Parsing;

namespace wasp.Compiling
{
    class FunctionsCompiler
    {
        readonly CodeSection codeSection = new CodeSection();
        readonly ExportSection exportSection = new ExportSection();
        readonly FunctionSection functionSection = new FunctionSection();
        readonly TypeSection typeSection = new TypeSection();

        public IEnumerable<ISection> CompileFunctions(IEnumerable<Function> functions)
        {
            var currentIndex = -1;
            foreach (var function in functions)
            {
                currentIndex++;

                typeSection.AddSignature(function.Signature);

                functionSection.AddFunction(function);

                codeSection.AddCodeBody(function.Codebody, currentIndex);

                if (function.Context == Context.Export)
                    exportSection.AddFunction(function, currentIndex);
            }
            yield return typeSection;
            yield return functionSection;
            yield return exportSection;
            yield return codeSection;
        }
    }
}