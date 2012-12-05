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

        }

        public void DeleteDocument(Document doc)
        {

        }

        public void OpenDocument(Document doc)
        {

        }

        public void CreateDocument(Document doc)
        {

        }
        
        public static void  Main(string[] args)
        {

            string test = "Hej\nMed\ndig";

            Document doc = new Document(test, "Kewins dokument", new User("Kewin", 1));



            Console.Out.WriteLine(doc.text);
            string[] testArray = doc.CreateTextArray();

            for (int i=0; i<testArray.Length; i++)
            {
                Console.Out.WriteLine("array["+i+"] : " +testArray[i]);
            }
            Console.Out.WriteLine(doc.CreateTextArray());
        
        }

    }
}
