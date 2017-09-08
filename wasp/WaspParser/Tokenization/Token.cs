using System.Collections.Generic;
using wasp.enums;

namespace wasp.Tokenization
{
    class Token
    {
        readonly HashSet<Tokens> nextValidTokens;

        public Tokens Id;

        public long Identifier;

        public Token(Tokens id, IEnumerable<Tokens> nextValidTokes)
        {
            Id = id;
            nextValidTokens = new HashSet<Tokens>(nextValidTokes);
        }

        public bool ValidNext(Token nextToken)
        {
            return nextValidTokens.Contains(nextToken.Id);
        }
    }
}