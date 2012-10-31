using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperMethodsTests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void ElapsedStringTest()
        {
            Assert.AreEqual("0 seconds", DateTime.Now.Elapsed().ToElapsedString());
            Assert.AreEqual("1 second", DateTime.Now.Subtract(new TimeSpan(0, 0, 1)).Elapsed().ToElapsedString());
            Assert.AreEqual("1 hour", DateTime.Now.Subtract(new TimeSpan(1, 1, 0)).Elapsed().ToElapsedString());
            Assert.AreEqual("2 hours", DateTime.Now.Subtract(new TimeSpan(2, 0, 0)).Elapsed().ToElapsedString());
            Assert.AreEqual("5 days", DateTime.Now.Subtract(new TimeSpan(5, 1, 0, 0)).Elapsed().ToElapsedString());
            Assert.AreEqual("6 months", DateTime.Now.Subtract(new TimeSpan(30 * 6, 1, 1, 1)).Elapsed().ToElapsedString());
            Assert.AreEqual("1 year", DateTime.Now.Subtract(new TimeSpan(365, 1, 1, 1, 1)).Elapsed().ToElapsedString());
            Assert.AreEqual("5 years", DateTime.Now.Subtract(new TimeSpan(5 * 365, 0, 0, 0)).Elapsed().ToElapsedString());
        }
    }
}
