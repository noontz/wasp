using wasp.Tokenization;

namespace wasp.Intermediate
{
    class Parameter
    {
        public Parameter(Token inputType, Token identifier, uint position)
        {
            Identifier = identifier;
            InputType = inputType;
            Position = position;
        }

        public uint Position { get; }

        public Token Identifier { get; }

        public Token InputType { get; }
    }
}