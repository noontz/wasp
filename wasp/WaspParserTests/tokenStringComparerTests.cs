using Microsoft.VisualStudio.TestTools.UnitTesting;
using wasp.Tokenization;

namespace waspTests
{
    [TestClass]
    public class tokenStringComparerTests
    {
        [TestMethod]
        public void EqualTest()
        {
            var tsA = new TokenString("Hello");
            var tsB = new TokenString("Hello");
            var tsC = new TokenString("HellO");
            var tsComparer = new TokenStringComparer();

            Assert.IsTrue(tsComparer.Equals(tsA, tsB));
            Assert.IsFalse(tsComparer.Equals(tsA, tsC));
        }
    }
}