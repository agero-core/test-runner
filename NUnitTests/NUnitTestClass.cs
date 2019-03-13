using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class NUnitTestClass
    {
        [Test]
        public void PassedTest()
        {
            Assert.AreEqual("value1", TestContext.Parameters["key1"]);
            Assert.AreEqual("value2", TestContext.Parameters["key2"]);
            Assert.AreEqual(null, TestContext.Parameters["key3"]);
        }

        [Test]
        public void FailedTest()
        {
            Assert.Fail("Error");
        }
    }
}
