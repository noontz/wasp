using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;

namespace wasmparser
{
    class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);

        static void Main()
        {
            var wasm = ConfigurationManager.AppSettings["wasm"];
            AttachConsole(-1);
            if (!File.Exists(wasm))
                Console.WriteLine($"{wasm} is not a valid filepath");
            else
            {
                var bytes = File.ReadAllBytes(wasm);
                var counter = 0;
                foreach (var entry in bytes)
                {
                    Console.Write("0x" + entry.ToString("X2") + ", ");
                    counter++;
                    if (counter % 10 == 0)
                        Console.Write(Environment.NewLine);
                }
            }
            Console.Write(Environment.NewLine);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
