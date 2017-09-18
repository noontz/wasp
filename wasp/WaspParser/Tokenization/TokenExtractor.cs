using System.Collections.Generic;
using System.IO;
using wasp.enums;

namespace wasp.Tokenization
{
    /// <summary>
    ///     Encapsulates logic for parsing file contents into ordered Tokens // TODO Errormessaging
    /// </summary>
    class TokenExtractor
    {
        /// <summary>
        ///     Maps TokenStrings to their respective Tokens
        /// </summary>
        readonly TokensMap map = new TokensMap();

        /// <summary>
        ///     Extracts Tokens from filecontens
        /// </summary>
        /// <param name="filePath">Path to the wasp file to parse</param>
        /// <returns>An ordered Ienumerable of Tokens for further parsing</returns>
        public IEnumerable<Token> ExtractTokens(string filePath)
        {
            if (!filePath.EndsWith(".wasp"))
                throw new FileLoadException($"{nameof(filePath)} {filePath} is not a wasp file");
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"{nameof(filePath)} {filePath} was not found");
            var returnImediate = false;
            var tokenBuffer = new TokenString();
            var imidiateToken = new Token(Tokens.None, 0);
            var counter = -1;
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                while (true)
                {
                    if (returnImediate)
                    {
                        returnImediate = false;
                        yield return imidiateToken;
                    }
                    var readInt = fileStream.ReadByte();
                    if (readInt == -1) break;
                    if (readInt > 127 || readInt < 33) continue;
                    var currentTokenString = new TokenString((byte) readInt);
                    var token = map.GetToken(currentTokenString);
                    if (token == Tokens.None)
                    {
                        tokenBuffer.AddCharacter((byte) readInt);
                        token = map.GetToken(tokenBuffer);
                        if (token == Tokens.None) continue;
                        tokenBuffer.Clear();
                        counter++;
                        yield return new Token(token, counter, tokenBuffer);
                        continue;
                    }
                    if (tokenBuffer.HasValue)
                    {
                        var newBuffer = new TokenString();
                        counter+=2;
                        imidiateToken = new Token(token, counter, newBuffer);
                        returnImediate = true;
                        var returnToken = new Token(tokenBuffer.IsNumber ? Tokens.Number : Tokens.Identifier, counter - 1, tokenBuffer);
                        tokenBuffer = newBuffer;
                        yield return returnToken;
                        continue;
                    }
                    counter++;
                    yield return new Token(token, counter);
                }
            }
        }
    }
}