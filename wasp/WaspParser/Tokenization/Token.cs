using wasp.enums;

namespace wasp.Tokenization
{
    struct Token
    {

        public Token(Tokens id)
        {
            Id = id;
            Value = null;
        }

        public Token(Tokens id, TokenString value)
        {
            Id = id;
            Value = value.ToString();
        }

        public Tokens Id;

        public string Value;

        public override string ToString()
        {
            return Id + " > " + Value;
        }
    }
}