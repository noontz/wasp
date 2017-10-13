using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wasp.enums;
using wasp.Intermediate;

namespace wasp.Compiling
{
    class FunctionsCompiler
    {
        public IEnumerable<Section> CompileFunctions(IEnumerable<Function> functions)
        {
            var sections = new Dictionary<ModuleSections, Section>
            {
                {ModuleSections.Type, new TypeSection() }
            };
            foreach (var function in functions)
            {
                ((TypeSection)sections[ModuleSections.Type]).AddSignature(function.Signature);
            }
            return sections.Values;
        }
    }
}
