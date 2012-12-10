using SliceOfPie;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;


namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for StorageTest and is intended
    ///to contain all StorageTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StorageTest
    {
        static Document testDoc;
        static Document testDoc1;
        static Document testDoc2;

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            // Test doc 0
            string docText = "This my new and fabulous blog where i would love to write about my dog called Fido!\n" +
                             "Fido is a really nice dog which sadly only has 3 legs, because one time where i was really angry, and i had this saw, well never mind.\n" +
                             "I love my dog above anything else in this world, and i thought i would dedicate this document to him!\n" +
                             "This is Fido: <img src=\"Fido.gif\" alt=\"My Dog\">";

            testDoc = new Document(docText, "Fido", new User("Karsten"));
            testDoc.Path = "root/cuteanimalsxoxo";


            // Test doc 1
            string docText1 = "New text\n" +
                             "more text";

            testDoc1 = new Document(docText1, "Herp", new User("Kewin"));
            testDoc1.Path = "root";


            // Test doc 2
            string docText2 = "Interesting facts about crocodiles: \n" +
                              "They bite hard: \n"+
                              "and thats it.";

            testDoc2= new Document(docText2, "Crocoman", new User("Johnny"));
            testDoc2.Path = "root/cuteanimalsxoxo/reptiles";

            





        }
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
        ///Tests if the delete function actually deletes a file from the filesystem
        ///by first adding a document and then removing the same one again.
        ///</summary>
        [TestMethod()]
        public void DeleteFileTest()
        {
            Storage.WriteToFile(testDoc);
            bool expected = File.Exists("root\\"+testDoc.Id + ".txt");
            Storage.DeleteFile(testDoc.Id);
            bool actual = File.Exists("root\\"+testDoc.Id + ".txt");
            Assert.AreNotEqual(expected, actual);

        }
        [TestMethod()]
        public void GetHierachyTest()
        {
            Storage.WriteToFile(testDoc);
            Storage.WriteToFile(testDoc1);
            Storage.WriteToFile(testDoc2);

            

            Folder root = new Folder("root");
            Folder cuteanimals = new Folder("cuteanimalsxoxo");
            Folder reptiles = new Folder("reptiles");

            DocumentStruct testStruct0 = new DocumentStruct(testDoc.Title, testDoc.Owner, testDoc.Id, testDoc.Path);
            DocumentStruct testStruct1 = new DocumentStruct(testDoc1.Title, testDoc1.Owner, testDoc1.Id, testDoc1.Path);
            DocumentStruct testStruct2 = new DocumentStruct(testDoc2.Title, testDoc2.Owner, testDoc2.Id, testDoc2.Path);

            reptiles.AddChild(testStruct2);
            cuteanimals.AddChild(testStruct0);
            root.AddChild(testStruct1);

            cuteanimals.AddChild(reptiles);
            root.AddChild(cuteanimals);
            Folder expected = root;
            Folder actual = Storage.GetHierachy();

            Assert.AreEqual(expected, actual);

        }

    }
}
