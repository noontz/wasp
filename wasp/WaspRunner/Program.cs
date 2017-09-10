using System;
using System.Collections.Generic;
using System.IO;
using wasp.enums;
using wasp.Tokenization;

namespace waspRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = @"D:\Repos\wasp\vscode\implementation_1.wasp";
            var extractor = new TokenExtractor();
            try
            {
                if (File.Exists(a))
                    foreach (var t in extractor.ExtractTokens(a))
                        Console.WriteLine(t.Id.ToString());
                ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            Console.ReadKey();
        }

        class TokenExtractor
        {
            readonly TokensMap map = new TokensMap();

            public IEnumerable<Token> ExtractTokens(string filePath)
            {
                Console.WriteLine("YOUR IN");

                var returnImediate = false;
                var tokenBuffer = new TokenString();
                var imidiateToken = new Token(Tokens.None);
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    while (true)
                    {
                        if (returnImediate)
                        {
                            returnImediate = false;
                            yield return imidiateToken;
                        }
                        var b = fs.ReadByte();
                        if (b == -1) break;
                        if (b > 127 || b < 33) continue;
                        var currentToken = new TokenString((byte) b);
                        var token = map.GetToken(currentToken);
                        if (token != Tokens.None)
                            if (!tokenBuffer.HasValue)
                            {
                                yield return new Token(token);
                            }
                            else
                            {
                                var newBuffer = new TokenString();
                                imidiateToken = new Token(token, newBuffer);
                                returnImediate = true;
                                var returnToken = new Token(tokenBuffer.IsNumber ? Tokens.Number : Tokens.Identifier,
                                    tokenBuffer);
                                tokenBuffer = newBuffer;
                                yield return returnToken;
                            }
                        else
                        {
                            tokenBuffer.AddCharacter((byte)b);
                            token = map.GetToken(tokenBuffer);
                            if (token == Tokens.None) continue;
                            tokenBuffer.Clear();
                            yield return new Token(token, tokenBuffer);
                        }
                        
                    }
                    Console.WriteLine("YOUR OUT");
                }
            }
        }
    }
}