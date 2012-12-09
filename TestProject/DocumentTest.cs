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
        ///A test for ShareWith and GetSharedWith.
        ///</summary>
        [TestMethod()]
        public void ShareWithTest()
        {
            User user1 = new User("kewin");

            Document doc1 = new Document("text", "Kewins dokument", user1);
            User user2 = new User("christian");
            User user3 = new User("kasra");
            doc1.ShareWith(user2);
            doc1.ShareWith(user3);

            List<User> expected = new List<User>();
            expected.Add(user2);
            expected.Add(user3);
            List<User> actual = doc1.SharedWith;

            Assert.IsTrue(expected[1] == actual[1] && expected[0] == actual[0]);
        }

        [TestMethod()]
        // Test a merging with 2 documents, where the new document has a line more than previous version, which should be added to the document.
        // The 3 following MergeWithTests also confirm that CreateTextArray is working as intended.
        public void MergeWithTest1()
        {
            string doc1text = "Dette er et dokument.\nDokumentet indeholder linjer.\nSenere kan det også indeholde billeder.";
            string doc2text = "Dette er et dokument.\nDokumentet indeholder linjer.\nHer tester jeg om en indsat linje kommer med.\nSenere kan det også indeholde billeder.";

            Document doc1 = new Document(doc1text, "Kewins dokument", new User("Kewin"));
            Document doc2 = new Document(doc2text, "Kewins dokument", new User("Kewin"));

            doc1.MergeWith(doc2, new User("Kewin"));

            bool compare = (String.Compare(doc1.Text, doc2text) == 0);

            Assert.IsTrue(compare);
        }
        [TestMethod()]
        // Test a merging with 2 documents, where the new document has a line less than the old one, which should not be added to the document.
        public void MergeWithTest2()
        {
            string doc1text = "Dette er et dokument.\nDokumentet indeholder linjer.\nSenere kan det også indeholde billeder.";
            string doc2text = "Dette er et dokument.\nDokumentet indeholder linjer.\nHer tester jeg om en indsat linje kommer med.\nSenere kan det også indeholde billeder.";

            Document doc1 = new Document(doc1text, "Kewins dokument", new User("Kewin"));
            Document doc2 = new Document(doc2text, "Kewins dokument", new User("Kewin"));

            doc2.MergeWith(doc1, new User("Kewin"));

            bool compare = (String.Compare(doc2.Text, doc1text) == 0);

            Assert.IsTrue(compare);
        }
        [TestMethod()]
        // Test a merge of two documents where almost every line changes.
        public void MergeWithTest3()
        {
            string doc1text = "Der var engang en dreng der hed Birger.\n" +
                              "Birger var en rigtig dum dreng og blev drillet i skolen.\n" +
                              "Det var der også god grund til fordi han var nemlig født med et mentalt handicap.\n" +
                              "Birger døde desværre i en meget voldelig sneboldskamp i skolen.\n" +
                              "Men det var nok for det bedre :)";
            string doc2text = "Der var engang en lille dreng der hed Kewin.\n" +
                              "Kewin var en rigtig rigtig dum dreng og blev drillet i skolen.\n" +
                              "Det var der også god grund til fordi han var nemlig født med et mentalt handicap og lav højde.\n" +
                              "Kewin døde desværre i en meget voldelig sneboldskamp i skolen.\n" +
                              "Men det var nok for det bedre :)";

            Document doc1 = new Document(doc1text, "Kewins dokument", new User("Kewin"));
            Document doc2 = new Document(doc2text, "Kewins dokument", new User("Kewin"));

            doc1.MergeWith(doc2, new User("Kewin"));

            bool compare = (String.Compare(doc1.Text, doc2text) == 0);

            Assert.IsTrue(compare);
        }

        [TestMethod()]
        // Test if it is actually the later of 2 entries that is retrieved.
        public void GetNewestEntryTest()
        {
            User user = new User("Kewin");
            Document doc1 = new Document("text", "Kewins dokument", user);
            doc1.Log.AddEntry(new Document.DocumentLog.Entry(user, "First entry", null));
            Document.DocumentLog.Entry entry1 = new Document.DocumentLog.Entry(user, "Second entry",null);
            doc1.Log.AddEntry(entry1);

            Document.DocumentLog.Entry entry2 = doc1.Log.GetNewestEntry();

            Assert.AreEqual(entry1, entry2);
        }


    }
    
}
