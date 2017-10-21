using System.Collections.Generic;
using wasp.enums;

namespace wasp.Tokenization
{
    /// <summary>
    ///     Maps TokenStrings to Tokens
    /// </summary>
    class TokensMap
    {
        /// <summary>
        ///     The Dictionary with the mappings
        /// </summary>
        readonly Dictionary<TokenString, Tokens> tokenMap;

        /// <summary>
        ///     Initialization of the map. All mappings are defined here
        /// </summary>
        public TokensMap() => tokenMap = new Dictionary<TokenString, Tokens>
        {
            {new TokenString("{"), Tokens.Lbracket},
            {new TokenString("}"), Tokens.Rbracket},
            {new TokenString("("), Tokens.Lparens},
            {new TokenString(")"), Tokens.Rparens},
            {new TokenString("+"), Tokens.Plus},
            {new TokenString("int"), Tokens.Int32},
            {new TokenString("export"), Tokens.Export},
            {new TokenString(","), Tokens.Comma},
            {new TokenString("return"), Tokens.Return},
            {new TokenString(";"), Tokens.SemiColon}
        };

        /// <summary>
        ///     Fetches the mapped Tokens of a TokenString
        /// </summary>
        /// <param name="tokenString">The TokenString to be mapped</param>
        /// <returns>A matched Tokens. Returns Tokens.None if no match</returns>
        public Tokens GetToken(TokenString tokenString) => tokenMap.TryGetValue(tokenString, out Tokens value)
            ? value
            : Tokens.None;
    }
}