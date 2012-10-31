//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using HelperMethods;
//namespace HelperMethodTests
//{
//    /// <summary>
//    ///This is a test class for StringExtensionsTest and is intended
//    ///to contain all StringExtensionsTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class StringParserTests
//    {
//        private TestContext testContextInstance;

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        /// <summary>
//        ///A test for AsDomain
//        ///</summary>
//        [TestMethod()]
//        public void FindLinkTest()
//        {
//            Assert.AreEqual("", HtmlParser.FindLink(""));
//            Assert.AreEqual("", HtmlParser.FindLink("hey buddy"));
//            Assert.AreEqual("http://bing.com", HtmlParser.FindLink("hey http://bing.com buddy"));
//            Assert.AreEqual("http://bing.com", HtmlParser.FindLink("http://bing.com buddy"));
//            Assert.AreEqual("http://bing.com", HtmlParser.FindLink("hey http://bing.com"));
//            Assert.AreEqual("http://bing.com", HtmlParser.FindLink("http://bing.com"));
//        }
//    }
//}