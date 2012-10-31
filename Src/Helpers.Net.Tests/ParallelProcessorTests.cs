using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace HelperMethodsTests
{
    /// <summary>
    /// Summary description for ParallelProcessorTests
    /// </summary>
    [TestClass]
    public class ParallelProcessorTests
    {
        public ParallelProcessorTests()
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

        //[TestMethod]
        //public void ForEachTest_SmallList()
        //{
        //    var smallList = new int[50];
        //    for (var i = 0; i < smallList.Length; i++)
        //        smallList[i] = i;

        //    // square each element of i
        //    smallList.EachParallel(i => i *= 2);

        //    // confirm the math
        //    for (var i = 0; i < smallList.Length; i++)
        //        Assert.AreEqual(i * i, smallList[i]);
        //}

        //[TestMethod]
        //public void ForEachTest_BigList()
        //{
        //    var biglist = new int[100000];
        //    for (var i = 0; i < biglist.Length; i++)
        //        biglist[i] = i;
        
        //    // square each element of i
        //    biglist.EachParallel(i => i *= 2);

        //    // confirm the math
        //    for (var i = 0; i < biglist.Length; i++)
        //        Assert.AreEqual(i * i, biglist[i]);
        //}
    }
}
