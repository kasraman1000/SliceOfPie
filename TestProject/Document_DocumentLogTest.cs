using SliceOfPie;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for Document_DocumentLogTest and is intended
    ///to contain all Document_DocumentLogTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Document_DocumentLogTest
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddEntry
        ///</summary>
        [TestMethod()]
        public void AddEntryTest()
        {
            User user = null; // TODO: Initialize to an appropriate value
            Document doc = null; // TODO: Initialize to an appropriate value
            Document.DocumentLog target = new Document.DocumentLog(user, doc); // TODO: Initialize to an appropriate value
            Document.DocumentLog.Entry entry = null; // TODO: Initialize to an appropriate value
            target.AddEntry(entry);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetNewestEntry
        ///</summary>
        [TestMethod()]
        public void GetNewestEntryTest()
        {
            User user = null; // TODO: Initialize to an appropriate value
            Document doc = null; // TODO: Initialize to an appropriate value
            Document.DocumentLog target = new Document.DocumentLog(user, doc); // TODO: Initialize to an appropriate value
            Document.DocumentLog.Entry expected = null; // TODO: Initialize to an appropriate value
            Document.DocumentLog.Entry actual;
            actual = target.GetNewestEntry();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
