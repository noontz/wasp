using System.Collections.Generic;
using wasp.Tokenization;

namespace wasp.enums
{
    static class TokenGroupMap
    {
        static readonly Dictionary<Tokens, TokenGroups> Map = new Dictionary<Tokens, TokenGroups>
        {
            {Tokens.None, TokenGroups.None},
            {Tokens.Plus, TokenGroups.Operator},
            {Tokens.Return, TokenGroups.Operator},
            {Tokens.Lbracket, TokenGroups.Punctuator},
            {Tokens.Rbracket, TokenGroups.Punctuator},
            {Tokens.Lparens, TokenGroups.Punctuator},
            {Tokens.Rparens, TokenGroups.Punctuator},
            {Tokens.Comma, TokenGroups.Literal},
            {Tokens.SemiColon, TokenGroups.Operator},
            {Tokens.Int32, TokenGroups.ValueType},
            {Tokens.Export, TokenGroups.Keyword},
            {Tokens.Identifier, TokenGroups.Literal},
            {Tokens.Number, TokenGroups.Literal}
        };

        public static  TokenGroups GetGroup(Token token) => Map[token.Id];
    }
}