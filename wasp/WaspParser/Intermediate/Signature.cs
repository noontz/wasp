using System.Collections.Generic;
using wasp.Global;
using wasp.Tokenization;

namespace wasp.Intermediate
{
    class Signature
    {
        public Token ReturnType;

        byte[] typeSectionIndex;

        public Signature() => Parameters = new List<Parameter>();

        public List<Parameter> Parameters { get; }

        public void AddParameter(Token inputType, Token identifier, int position) => Parameters.Add(new Parameter(inputType, identifier,(uint)position));

        public void SetIndex(int position) => typeSectionIndex = Leb128.VarUint32((uint) position);
    }
}