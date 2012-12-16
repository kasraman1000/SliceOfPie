using SliceOfPie;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;


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
        static Project testproject;
        static Project testproject2;

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

            testDoc2= new Document(docText2, "Crocoman", new User("Karsten"));
            testDoc2.Path = "root/cuteanimalsxoxo/reptiles";

            
            testproject = new Project("Projekt", new User("Kewin"), new List<User>(){new User("Karsten") });
            testproject2 = new Project("Projekt2", new User("Karsten"), new List<User>() { new User("Kewin") });




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

            Project proj = new Project("Project", new User("owner"), new List<User>(), "Project1");

            Storage.SaveProjectToFile(proj);

            Storage.WriteToFile(proj, documentToBeWritten);

            Document readDocument = Storage.ReadFromFile("Project1","documentID");

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

            Project proj = new Project("Project2", new User("owner"), new List<User>(), "Project2");

            Storage.SaveProjectToFile(proj);

            Storage.WriteToFile(proj, documentToBeWritten);

            Document readDocument = Storage.ReadFromFile("Project2", "documentID");

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
        public void DeleteDocumentTest()
        {
            Project project = new Project("Projekt", new User("NotKewin"), new List<User>() { new User("Karsten") });
            Storage.SaveProjectToFile(project);
            Storage.WriteToFile(project, testDoc1);
            bool expected = File.Exists(project.Id+"\\"+testDoc1.Id + ".txt");
            Storage.DeleteDocument(project.Id, testDoc1.Id);
            bool actual = File.Exists(project.Id + "\\" + testDoc1.Id + ".txt");
            Assert.AreNotEqual(expected, actual);

        }
        [TestMethod()]
        public void GetHierachyTest()
        {
            Project project = new Project("Projekt1", new User("NotKewin2"), new List<User>() { new User("Karsten") });
            Storage.SaveProjectToFile(project);

            Storage.WriteToFile(project, testDoc);
            Storage.WriteToFile(project, testDoc1);
            Storage.WriteToFile(project, testDoc2);

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
            project.AddChild(cuteanimals);
            Project expected = project;
            Project actual = Storage.GetHierachy(project.Id);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void SaveAndDeleteProjectTest()
        {
            Project project = new Project("Projekt1", new User("NotKewin3"), new List<User>() { new User("Karsten") });
            Storage.SaveProjectToFile(project);
            string currentDir = Directory.GetCurrentDirectory();
            bool expected = Directory.Exists(currentDir + "\\" + project.Id);
            Storage.DeleteProject(project.Id);
            bool actual = Directory.Exists(currentDir + "\\" + project.Id);

            Assert.AreNotEqual(expected, actual);

        }

        [TestMethod()]
        public void GetAllProjectsTest()
        {
            Storage.SaveProjectToFile(testproject);
            Storage.SaveProjectToFile(testproject2);

            List<Project> expected = new List<Project>(){testproject2,testproject};

            List<Project> actual = Storage.GetAllProjects();

            CollectionAssert.AreEqual(actual, expected);
        }

    }
}
