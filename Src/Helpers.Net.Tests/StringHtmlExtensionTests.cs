using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pfizer.ECFv2.Portals.PfizerWorld.Common.Test
{
    /// <summary>
    /// Summary description for StringHtmlExtensionTests
    /// </summary>
    [TestClass]
    public class StringHtmlExtensionTests
    {
        public StringHtmlExtensionTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ToDelimitedStringTest()
        {
            Assert.AreEqual("", (new string[] { }).ToDelimitedString(","));
            Assert.AreEqual("", ((string[])null).ToDelimitedString(","));
            Assert.AreEqual("one", (new string[] { "one" }).ToDelimitedString(", "));
            Assert.AreEqual("one, two", (new string[] { "one", "two" }).ToDelimitedString(", "));
            Assert.AreEqual("one,two", (new string[] { "one", "two" }).ToDelimitedString(","));
        }

        [TestMethod]
        public void StripHtmlTest()
        {
            Assert.IsNull(((string)null).StripHtml());

            Assert.AreEqual("hello", "hello".StripHtml());

            Assert.AreEqual("hello", "he<b>ll</b>o".StripHtml());
        }

        [TestMethod]
        public void TruncateTextTest()
        {
            Assert.IsNull(((string)null).StripHtml());

            string test = "1234567890";
            Assert.AreEqual("12345", test.Truncate(5, null));
            Assert.AreEqual("12345...", test.Truncate(5, "..."));
            Assert.AreEqual(string.Empty, string.Empty.Truncate(5, null));
            Assert.AreEqual("12", "12".Truncate(5));
        }

        [TestMethod]
        public void TruncateHtmlEmptyTest()
        {
            Assert.IsNull(((string)null).TruncateHtml(100));
            Assert.AreEqual(string.Empty.TruncateHtml(100), string.Empty);
        }

        [TestMethod]
        public void TruncateHtmlTextTest()
        {
            // no html test
            Assert.AreEqual("abc".TruncateHtml(10), "abc");
            Assert.AreEqual("abc".TruncateHtml(2), "ab");
        }

        [TestMethod]
        public void TruncateHtmlTest()
        {
            var html = @"<p>aaa <b>bbb</b>
ccc<br> ddd</p>";

            Assert.AreEqual(@"<p>aaa <b>bbb</b>
ccc<br> ddd</p>", html.TruncateHtml(100, "no trailing text")); // it ignores unclosed tags

            Assert.AreEqual(@"<p>aaa <b>bbb</b>
ccc<br>...</br></p>", html.TruncateHtml(14, "..."));

            Assert.AreEqual(@"<p>aaa <b>bbb</b></p>", html.TruncateHtml(10));

            // self closing test
            html = @"<p>hello<br/>there</p>";
            Assert.AreEqual(@"<p>hello<br/>th</p>", html.TruncateHtml(7));

            Assert.AreEqual("<b>i'm</b>", "<b>i'm awesome</b>".TruncateHtml(8));
            Assert.AreEqual("<b>i'm...</b>", "<b>i'm awesome</b>".TruncateHtml(8, "..."));
        }

        [TestMethod]
        public void TruncateWordsTest()
        {
            Assert.IsNull(((string)null).TruncateWords(100));
            Assert.AreEqual(string.Empty, string.Empty.TruncateWords(100));

            Assert.AreEqual("big brown", "big brown beaver".TruncateWords(12));
            Assert.AreEqual("big...", "big brown beaver".TruncateWords(5, "..."));
        }

        [TestMethod]
        public void TruncateWordsBreakingHtmlTagTest()
        {
            // truncates in the middle of a tag
            Assert.AreEqual("<b>i'm", "<b>i'm awesome</b>".TruncateWords(16));
        }
    }
}
