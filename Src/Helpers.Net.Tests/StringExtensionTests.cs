using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HelperMethodTests
{
    /// <summary>
    ///This is a test class for StringExtensionsTest and is intended
    ///to contain all StringExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringExtensionsTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void ToAbbreviatedStringTest()
        {
            Assert.AreEqual("1", 1.ToAbbreviatedString());
            Assert.AreEqual("2k", 2000.ToAbbreviatedString());
            Assert.AreEqual("666k", 666666.ToAbbreviatedString());
            Assert.AreEqual("1.5M", 1500000.ToAbbreviatedString());
        }

        [TestMethod]
        public void LongestWordTest()
        {
            Assert.AreEqual("beaver", "the big brown beaver".LongestWord());
            Assert.AreEqual("beaver", "beaver".LongestWord());
        }

        /// <summary>
        ///A test for AsDomain
        ///</summary>
        [TestMethod()]
        public void AsDomainTest1()
        {
            Assert.IsNull(((string)null).AsDomain());
            Assert.AreEqual(string.Empty, string.Empty.AsDomain());
            Assert.AreEqual("http://www.bing.com", "http://www.bing.com/hello".AsDomain());
            Assert.AreEqual("http://localhost:1234", "http://localhost:1234/hello".AsDomain());
            Assert.AreEqual("http://www.bing.com", "http://www.bing.com".AsDomain());
            Assert.AreEqual("https://www.bing.com", "https://www.bing.com/hello".AsDomain());

            Assert.AreEqual("/relative", "/relative".AsDomain());
        }

        /// <summary>
        ///A test for AsDomain
        ///</summary>
        [TestMethod()]
        public void AsDomainTest()
        {
            Assert.AreEqual("http://bing.com", new Uri("http://bing.com/hello").AsDomain());
        }

        /// <summary>
        ///A test for AsDomain
        ///</summary>
        [TestMethod()]
        public void ContainsPhraseTest()
        {
            string msg = "hey #dude, what's up?";
            Assert.IsTrue(msg.ContainsPhrase("#dude"));
            Assert.IsTrue(msg.ContainsPhrase("what's up"));
            Assert.IsTrue(msg.ContainsPhrase("up?"));
            Assert.IsFalse(msg.ContainsPhrase("yuengling"));
        }
    }
}