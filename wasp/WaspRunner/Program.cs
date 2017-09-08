using System;
using System.IO;
using wasp.Tokenization;

namespace waspRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenString currentToken;
            using (var fs = new FileStream(@"D:\Repos\wasp\vscode\implementation_1.wasp", FileMode.OpenOrCreate,FileAccess.Read))
            {
                while (true)
                {
                    var b = fs.ReadByte();
                    if (b == -1) break;
                    var t = new TokenString((byte)b);
                    Console.WriteLine(b);
                }
                Console.WriteLine("YOUR OUT");
            }
            Console.ReadKey();
        }
    }
}