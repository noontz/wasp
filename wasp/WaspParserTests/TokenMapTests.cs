using Microsoft.VisualStudio.TestTools.UnitTesting;
using wasp;
using wasp.enums;
using wasp.Tokenization;

namespace waspTests
{
    [TestClass]
    public class TokenMapTests
    {
        [TestMethod]
        public void CheckTokensTest()
        {
            var map = new TokensMap();
            var lB = new TokenString("{");
            var rB = new TokenString("}");
            var lP = new TokenString("(");
            var rP = new TokenString(")");
            var pl = new TokenString("+");
            var i32 = new TokenString("int");
            var export = new TokenString("export");

            Assert.IsTrue(map.GetToken(lB) == Tokens.Lbracket);
            Assert.IsTrue(map.GetToken(rB) == Tokens.Rbracket);
            Assert.IsTrue(map.GetToken(lP) == Tokens.Lparens);
            Assert.IsTrue(map.GetToken(rP) == Tokens.Rparens);
            Assert.IsTrue(map.GetToken(pl) == Tokens.Plus);
            Assert.IsTrue(map.GetToken(i32) == Tokens.Int32);
            Assert.IsTrue(map.GetToken(export) == Tokens.Export);
        }
    }
}