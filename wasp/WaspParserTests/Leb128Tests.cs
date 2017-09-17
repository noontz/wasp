using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wasp.Global;
using wasp.Tokenization;

// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ExpressionIsAlwaysNull

namespace waspTests
{
    [TestClass]
    public class Leb128Tests
    {
        [TestMethod]
        public void VarUint32Test()
        {
            const string goal = "0xE58E26";
            var resultBytes = Leb128.VarUint32(624485);
            var result = "0x";
            for (byte i = 0; i < resultBytes.Length; i++)
                result += resultBytes[i].ToString("X");
            Assert.IsTrue(result == goal);
        }

        [TestMethod]
        public void VarInt32Test()
        {
            const string goal = "0x9BF159";
            var resultBytes = Leb128.VarInt32(-624485);
            var result = "0x";
            for (byte i = 0; i < resultBytes.Length; i++)
                result += resultBytes[i].ToString("X");
            Assert.IsTrue(result == goal);
        }
    }
}