using System.Collections.Generic;
using wasp.Global;
using wasp.Tokenization;

namespace wasp.Intermediate
{
    class Signature
    {
        public Token Identifier { get; }

        public Token ReturnType;

        public byte[] TypeIndex;

        public Signature(Token identifier)
        {
            Parameters = new List<Parameter>();
            Identifier = identifier;
        }

        public List<Parameter> Parameters { get; }

        public void AddParameter(Token inputType, Token identifier, int position) => Parameters.Add(new Parameter(inputType, identifier,(uint)position));

        public void SetIndex(int position) => TypeIndex = Leb128.VarUint32((uint) position);
    }
}