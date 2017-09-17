using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wasp.Tokenization;

namespace waspTests
{
    [TestClass]
    public class TokenExtractorTests
    {
        const string folder = @"D:\Repos\wasp\wasp\WaspParserTests\testfiles\";
        [TestMethod]
        public void ExtractTest_1()
        {
            var tokenExtractor = new TokenExtractor();
            string result = "";
            var tokens = tokenExtractor.ExtractTokens(folder + "token_1.wasp");
            foreach (var token in tokens)
                result += token + Environment.NewLine;
           
            var goal = File.ReadAllText(folder + "token_1.txt");
            goal += Environment.NewLine;


            Assert.IsTrue(result == goal);
        }
    }
}
