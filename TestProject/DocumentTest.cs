using SliceOfPie;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for DocumentTest and is intended
    ///to contain all DocumentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DocumentTest
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
        ///A test for ShareWith
        ///</summary>
        [TestMethod()]
        public void ShareWithTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            User owner = null; // TODO: Initialize to an appropriate value
            Document target = new Document(text, title, owner); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            target.ShareWith(user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for MergeWith
        ///</summary>
        [TestMethod()]
        public void MergeWithTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            User owner = null; // TODO: Initialize to an appropriate value
            Document target = new Document(text, title, owner); // TODO: Initialize to an appropriate value
            Document doc = null; // TODO: Initialize to an appropriate value
            target.MergeWith(doc);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetTitle
        ///</summary>
        [TestMethod()]
        public void GetTitleTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            User owner = null; // TODO: Initialize to an appropriate value
            Document target = new Document(text, title, owner); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetTitle();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetText
        ///</summary>
        [TestMethod()]
        public void GetTextTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            User owner = null; // TODO: Initialize to an appropriate value
            Document target = new Document(text, title, owner); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetText();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSharedWith
        ///</summary>
        [TestMethod()]
        public void GetSharedWithTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            User owner = null; // TODO: Initialize to an appropriate value
            Document target = new Document(text, title, owner); // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = target.GetSharedWith();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetOwner
        ///</summary>
        [TestMethod()]
        public void GetOwnerTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            User owner = null; // TODO: Initialize to an appropriate value
            Document target = new Document(text, title, owner); // TODO: Initialize to an appropriate value
            User expected = null; // TODO: Initialize to an appropriate value
            User actual;
            actual = target.GetOwner();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetLog
        ///</summary>
        [TestMethod()]
        public void GetLogTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            User owner = null; // TODO: Initialize to an appropriate value
            Document target = new Document(text, title, owner); // TODO: Initialize to an appropriate value
            Document.DocumentLog expected = null; // TODO: Initialize to an appropriate value
            Document.DocumentLog actual;
            actual = target.GetLog();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EditText
        ///</summary>
        [TestMethod()]
        public void EditTextTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            User owner = null; // TODO: Initialize to an appropriate value
            Document target = new Document(text, title, owner); // TODO: Initialize to an appropriate value
            string text1 = string.Empty; // TODO: Initialize to an appropriate value
            target.EditText(text1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CreateTextArray
        ///</summary>
        [TestMethod()]
        public void CreateTextArrayTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            User owner = null; // TODO: Initialize to an appropriate value
            Document target = new Document(text, title, owner); // TODO: Initialize to an appropriate value
            string[] expected = null; // TODO: Initialize to an appropriate value
            string[] actual;
            actual = target.CreateTextArray();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ChangeName
        ///</summary>
        [TestMethod()]
        public void ChangeNameTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            User owner = null; // TODO: Initialize to an appropriate value
            Document target = new Document(text, title, owner); // TODO: Initialize to an appropriate value
            string newName = string.Empty; // TODO: Initialize to an appropriate value
            target.ChangeName(newName);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
