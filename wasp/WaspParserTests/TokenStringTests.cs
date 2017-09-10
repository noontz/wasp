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
                string a = null;
                new TokenString(a);
            }
            catch (Exception e)
            {
                es[0] = e;
            }
            try
            {
                const string a = "123456789";
                new TokenString(a);
            }
            catch (Exception e)
            {
                es[1] = e;
            }
            try
            {
                const string a = "";
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