using System.Collections.Generic;
using wasp.enums;

namespace wasp.Tokenization
{
    class TokensMap
    {
        readonly Dictionary<TokenString, Tokens> tokenMap;

        public TokensMap() => tokenMap = new Dictionary<TokenString, Tokens>
        {
            {new TokenString("{"), Tokens.Lbracket},
            {new TokenString("}"), Tokens.Rbracket},
            {new TokenString("("), Tokens.Lparens},
            {new TokenString(")"), Tokens.Rparens},
            {new TokenString("+"), Tokens.Plus},
            {new TokenString("int"), Tokens.Int32},
            {new TokenString("export"), Tokens.Export},
            {new TokenString(","), Tokens.Comma}
        };

        public Tokens GetToken(TokenString tokenString) => tokenMap.TryGetValue(tokenString, out Tokens value) ? value : Tokens.None;
    }
}