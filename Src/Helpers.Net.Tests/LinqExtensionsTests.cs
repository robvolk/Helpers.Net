using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperMethodsTests
{
    /// <summary>
    /// Summary description for LinqExtensions
    /// </summary>
    [TestClass]
    public class LinqExtensionsTests
    {
        public LinqExtensionsTests()
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
        public void ExistsTest()
        {
            Assert.IsFalse(new List<string>().Exists("haha")); // empty case

            int[] list = { 1, 2, 3, 4 };
            Assert.IsTrue(list.Exists(1));
            Assert.IsFalse(list.Exists(99));
        }

        [TestMethod]
        public void ExistsPredicateTest()
        {
            Assert.IsFalse(new List<string>().Exists(l => l == "haha")); // empty case

            int[] list = { 1, 2, 3, 4 };

            Assert.IsTrue(list.Exists(l => l == 2));
            Assert.IsFalse(list.Exists(l => l == 99));
        }



        class Person
        {
            public Address Address { get; set; }
        }
        class Address
        {
            public string State { get; set; }
        }

        [TestMethod]
        public void ObjectSelectTest1()
        {
            var sb = new StringBuilder();
            sb.Select(it => it.Append(34));
            Assert.AreEqual("34", sb.ToString());
        }

        [TestMethod]
        public void ObjectSelectTest2()
        {
            var sb = new StringBuilder();
            var s = sb.Select(it => it.Append(34)).ToString();
            Assert.AreEqual("34", s);

            Assert.AreEqual("34", new StringBuilder().Select(it => it.Append(34)).ToString());

            var person = new Person { Address = new Address { State = "NY" } };
            var state = person.Select(p => p.Address).Select(a => a.State);
            Assert.AreEqual("NY", state);
        }

        [TestMethod]
        public void AsEnumerableTest()
        {
            var person = new Person { Address = new Address { State = "NY" } };

            var state = person.AsEnumerable().Select(p => p.Address).AsEnumerable().Select(a => a.State).Single();
            Assert.AreEqual("NY", state);
        }

        private static Tuple<Person, Person> GetCouple()
        {
            var nyPerson = new Person { Address = new Address { State = "NY" } };
            var caPerson = new Person { Address = new Address { State = "CA" } };

            return Tuple.Create(nyPerson, caPerson);
        }

        [TestMethod]
        public void TupleBindTest()
        {
            GetCouple().Bind((nyPerson, caPerson) =>
            {
                Assert.AreEqual("NY", nyPerson.Address.State);
                Assert.AreEqual("CA", caPerson.Address.State);
            });
        }

        [TestMethod]
        public void TryTest()
        {
            // examples for blog
            string state = null;
            var person = new Person { Address = new Address { State = "NY" } };

            //// we'd like to do this:
            //state = person.Address.State;

            // but what if person or person.Address is null?
            if (person != null && person.Address != null)
                state = person.Address.State;

            // or 
            state = person != null && person.Address != null ? person.Address.State : null;

            // finally, this is easier!
            state = person.IfNotNull(p => p.Address).IfNotNull(a => a.State);

            // ideally we'd have a new C# operator like Phil Haack suggested, but we don't.
            //state = person.?Address.?State;

            // TODO: write unit tests
            Assert.AreEqual("NY", state);
        }
    }
}
