using wasp.enums;

namespace wasp.Tokenization
{
    struct Token
    {

        public Token(Tokens id)
        {
            Id = id;
            Value = new TokenString();
        }

        public Token(Tokens id, TokenString value)
        {
            Id = id;
            Value = value;
        }

        public Tokens Id;

        public TokenString Value;

        public override string ToString() => $"{Id}{(Value.HasValue ? ":" : "")}{Value}";
    }
}