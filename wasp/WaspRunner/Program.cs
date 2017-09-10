using System;
using System.Diagnostics;
using System.Linq;
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
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var t = extractor.ExtractTokens(a).ToArray();
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            Console.ReadKey();
        }
    }
}