using SliceOfPie;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for StorageTest and is intended
    ///to contain all StorageTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StorageTest
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


        // Test whether the the document we write to the file system is the same when we read it again.
        [TestMethod()]
        public void WriteToAndReadFromFileTest()
        {
            Document documentToBeWritten = new Document("text", "title", new User("owner"), "documentID");

            Storage.WriteToFile(documentToBeWritten);

            Document readDocument = Storage.ReadFromFile("documentID");

            bool textBool = String.Compare(documentToBeWritten.Text, readDocument.Text) == 0;
            bool titleBool = String.Compare(documentToBeWritten.Title, readDocument.Title) == 0;
            bool pathBool = String.Compare(documentToBeWritten.Path, readDocument.Path) == 0;

            bool logBool = true;

          


            bool descriptionBool = true;
            bool userBool = true;
            bool changeLogBool = true;
            bool countBool = true;

            if (readDocument.Log.entries.Count != 1)
                countBool = false;

            if (String.Compare(readDocument.Log.entries[0].Description, documentToBeWritten.Log.entries[0].Description) != 0)
                descriptionBool = false;

            if (String.Compare(readDocument.Log.entries[0].User.ToString(), documentToBeWritten.Log.entries[0].User.ToString()) != 0)
                userBool = false;

            if (String.Compare(readDocument.Log.entries[0].Description, documentToBeWritten.Log.entries[0].Description) != 0)
                descriptionBool = false;


            Assert.IsTrue(changeLogBool&&countBool&&descriptionBool&&logBool&&logBool&&pathBool&&textBool&&titleBool&&userBool);
        }

        // Test whether or not some empty fields cause an issue.
        [TestMethod()]
        public void WriteToAndReadFromFileTest2()
        {
            Document documentToBeWritten = new Document("", "", new User("owner"), "documentID");


            Storage.WriteToFile(documentToBeWritten);

            Document readDocument = Storage.ReadFromFile("documentID");

            bool textBool = String.Compare(documentToBeWritten.Text, readDocument.Text) == 0;
            bool titleBool = String.Compare(documentToBeWritten.Title, readDocument.Title) == 0;
            bool pathBool = String.Compare(documentToBeWritten.Path, readDocument.Path) == 0;
            bool logBool = true;                   
            bool descriptionBool = true;
            bool userBool = true;
            bool changeLogBool = true;
            bool countBool = true;

            if (readDocument.Log.entries.Count != 1)
                countBool = false;

            if (String.Compare(readDocument.Log.entries[0].Description, documentToBeWritten.Log.entries[0].Description) != 0)
                descriptionBool = false;

            if (String.Compare(readDocument.Log.entries[0].User.ToString(), documentToBeWritten.Log.entries[0].User.ToString()) != 0)
                userBool = false;

            if (String.Compare(readDocument.Log.entries[0].Description, documentToBeWritten.Log.entries[0].Description) != 0)
                descriptionBool = false;


            Assert.IsTrue(changeLogBool && countBool && descriptionBool && logBool && logBool && pathBool && textBool && titleBool && userBool);
        }
       


        /// <summary>
        ///A test for DeleteFile
        ///</summary>
        [TestMethod()]
        public void DeleteFileTest()
        {
            string title = string.Empty; // TODO: Initialize to an appropriate value
            //Storage.DeleteFile(title);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
