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

            var typeBytes = sections.First().Compile();

            return;

            using (var fileStream = new FileStream(wasp, FileMode.Create, FileAccess.Write))
            {
                using (var binaryWriter = new BinaryWriter(fileStream))
                {
                    // preamble
                    binaryWriter.Write(Magic);
                    binaryWriter.Write(Version);
                    // sections
                    foreach (var section in sections.OrderBy(section => section.ID))
                        foreach (var currentByte in section.Compile())
                            binaryWriter.Write(currentByte);
                }
            }
        }
    }
}