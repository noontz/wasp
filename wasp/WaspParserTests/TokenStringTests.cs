using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wasp;
using wasp.Tokenization;

// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ExpressionIsAlwaysNull

namespace waspTests
{
    [TestClass]
    public class TokenStringTests
    {
        [TestMethod]
        public void HashCodeTest()
        {
            var a = new byte[] { 0x01, 0x02 };
            var b = new byte[] { 0x01, 0x02 };

            var aS = new TokenString(a);
            var bS = new TokenString(b);
            var c = new TokenString("Check");
            var d = new TokenString("Check");

            Assert.IsTrue(aS.GetHashCode() == bS.GetHashCode());
            Assert.IsTrue(c.GetHashCode() == d.GetHashCode());
        }

        [TestMethod]
        public void StringTest()
        {
            const string goal = "Hello";
            var token = new TokenString(goal);
            var result = token.ToString();
            Assert.IsTrue( result == goal);
        }

        [TestMethod]
        public void ConstructorTest_Bytes()
        {
            var es = new Exception[3];
            try
            {
               byte[] a = null;
               new TokenString(a);
            }
            catch (Exception e)
            {
                es[0] = e;
            }
            try
            {
                byte[] a = {1,2,3,4,5,6,7,8,9};
                new TokenString(a);
            }
            catch (Exception e)
            {
                es[1] = e;
            }
            try
            {
                byte[] a = { };
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
        public void ConstructorTest_Byte()
        {
            var goal = "P";
            var chars = Encoding.UTF8.GetBytes(goal);
            var tokenString = new TokenString(chars[0]);
            Assert.IsTrue(tokenString.ToString() == goal);
        }

    }
}
