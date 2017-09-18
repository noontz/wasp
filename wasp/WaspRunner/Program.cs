using System;
using System.Diagnostics;
using System.IO;
using wasp;
using wasp.Linking;
using wasp.Tokenization;

namespace waspRunner
{
    class Program
    {
        static void Main()
        {



            TestTokenParser();
            
            Console.ReadKey();
        }

        static void Process_Exited(object sender, EventArgs e)
        {
            if(!File.Exists("check.wast"))
                return;
            foreach (var line in File.ReadLines("check.wast"))
            {
                Console.WriteLine(line);
            }
        }

        public static void TestTokenParser()
        {
            var a = @"D:\Repos\wasp\vscode\implementation_1.wasp";
            var extractor = new TokenExtractor();

            var tokenParser = new TokenParser();

            tokenParser.Run(extractor.ExtractTokens(a));
        }

        public static void TestCompiler()
        {
            File.Delete("check.wast");
            API.Compile("", "compiled.wasm");
            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo
                {
                    FileName = "wasm2wast",
                    Arguments = "compiled.wasm -o check.wast",
                    RedirectStandardOutput = true,
                    CreateNoWindow = false
                }
            };
            process.Exited += Process_Exited;
            process.Start();
            //Thread.Sleep(10000);
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                Console.WriteLine(line);
            }
        }

        public static void TestTokenizer()
        {
            var a = @"D:\Repos\wasp\vscode\implementation_1.wasp";
            var extractor = new TokenExtractor();
            try
            {
                foreach (var extractToken in extractor.ExtractTokens(a))
                {
                    Console.WriteLine(extractToken);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    
}