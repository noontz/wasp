using System;
using System.Collections.Generic;
using System.Text;
using wasp.enums;
using wasp.Tokenization;

namespace wasp.Parsing
{
    class SignatureIntermediate
    {
        public Token ReturnType;

        public ArgumentIntermediate[] Arguments;
    }

    class ArgumentIntermediate
    {
        public ArgumentIntermediate(Token inputType, Token identifier)
        {
            Identifier = identifier;

            InputType = inputType;
        }

        public Token Identifier { get; }

        public Token InputType { get; }
    }
}
