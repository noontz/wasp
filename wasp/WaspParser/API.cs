using System;
using System.IO;
using System.Linq;
using wasp.Parsing;
using wasp.Tokenization;

namespace wasp
{
    static class API
    {
        static readonly byte[] Magic = {0x00, 0x61, 0x73, 0x6D};

        static readonly byte[] Version = {0x01, 0x00, 0x00, 0x00};

        public static void Compile(string wasp, string wasm)
        {
            var extractor = new TokenExtractor();

            var tokens = extractor.ExtractTokens(wasp);

            var tokenParser = new TokenParser();

            var sections = tokenParser.Run(tokens);

            Console.WriteLine();

            //foreach (var section in sections)
            //{
            //    var result = section.Compile();
            //    Console.WriteLine(section.ID);
            //    Console.Write("0x ");
            //    foreach (var b in result)
            //    {
            //        Console.Write(b.ToString("X2") + " ");
            //    }
            //    Console.WriteLine();
            //}



            using (var fileStream = new FileStream(wasm, FileMode.Create, FileAccess.Write))
            {
                using (var binaryWriter = new BinaryWriter(fileStream))
                {
                    // preamble
                    binaryWriter.Write(Magic);
                    binaryWriter.Write(Version);
                    // sections
                    foreach (var section in sections)
                        foreach (var currentByte in section.Compile())
                            binaryWriter.Write(currentByte);
                }
            }
        }
    }
}