using System.Collections.Generic;
using wasp.enums;

namespace wasp.Compiling
{
    interface  ISection
    {
       ModuleSections ID { get; }

       IEnumerable<byte> Compile();
    }
}