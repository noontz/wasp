using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wasp.Tokenization;

// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ExpressionIsAlwaysNull

namespace waspTests
{
    [TestClass]
    public class TokenStringTests
    {
        [TestMethod]
        public void StringTest()
        {
            const string goal = "Hello";
            var token = new TokenString(goal);
            var result = token.ToString();
            Assert.IsTrue(result == goal);
        }

        [TestMethod]
        public void ConstructorTest_String()
        {
            var es = new Exception[3];
            try
            {
                const string a = "a3bb";
                new TokenString(a);
            }
            catch (Exception e)
            {
                es[0] = e;
            }
            try
            {
                const string a = "aa9";
                new TokenString(a);
            }
            catch (Exception e)
            {
                es[1] = e;
            }
            try
            {
                const string a = "abcdefghi";
                new TokenString(a);
            }
            catch (Exception e)
            {
                es[2] = e;
            }
            foreach (var e in es)
            {
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AddCharacterTest()
        {
            var goal = "Hello";
            byte o = 0x6F;
            var tokenString = new TokenString("Hell");
            tokenString.AddCharacter(o);
            Assert.IsTrue(tokenString.ToString() == goal);
        }
    }
}