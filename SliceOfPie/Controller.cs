using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    class Controller
    {
        User activeUser;
        Folder rootFolder;

        public void SaveDocument(Document doc)
        {
            Storage.WriteToFile(doc);
        }

        public void DeleteDocument(Document doc)
        {
            Storage.DeleteFile(doc);
        }

        public void OpenDocument(Document doc)
        {
            Storage.ReadFromFile(doc);
            // And do something with it.
        }

        public void CreateDocument(Document doc)
        {

        }
        /*
        public static void Main(string[] args)
        {

            Document doc = new Document("Hej\nMed\ndig", "Kewins dokument", new User("Kewin", 1));
            Document.DocumentLog log = doc.GetLog();
            Document.DocumentLog.Entry e = log.GetNewestEntry();
            Console.Out.WriteLine(e.ToString());



            // Test af MergeWith

            string doc1text = "Dette er et dokument.\nDokumentet indeholder linjer.\nSenere kan det også indeholde billeder.";
            string doc2text = "Dette er et dokument.\nDokumentet indeholder linjer.\nHer tester jeg om en indsat linje kommer med.\nSenere kan det også indeholde billeder.";

            Document doc1 = new Document(doc1text, "Kewins dokument", new User("Kewin", 1));
            Document doc2 = new Document(doc2text, "Kewins dokument", new User("Kewin", 1));

            doc2.MergeWith(doc1);

            Console.Out.WriteLine(doc1.GetText());


            //Test af createTextArray

            string s = doc.GetText();
            Console.Out.WriteLine(s);
            string[] testArray = doc.CreateTextArray();

            for (int i = 0; i < testArray.Length; i++)
            {
                Console.Out.WriteLine("array[" + i + "] : " + testArray[i]);
            }
            Console.Out.WriteLine(doc.CreateTextArray());
        }
        */
    }

}

