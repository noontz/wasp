using wasp.enums;

namespace wasp.Tokenization
{
    class Token
    {
        public Token(Tokens id, int position)
        {
            ID = id;
            Position = position;
        }

        public Token(Tokens id, int position, TokenString value) : this(id, position) => Value = value;

        public int Position { get; }

        public TokenGroups Group => TokenGroupMap.GetGroup(this);

        public Tokens ID { get; }

        public TokenString Value { get; }

        public override string ToString() => $"{Position.ToString().PadRight(3)} > {Group.ToString().PadRight(12)}{ID}{(Value.HasValue ? " > " : "")}{Value}";
    }
}