using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SliceOfPie
{
    class Storage
    {
        public static void Main(string[] args)
        {
            Document testDoc = new Document(
            WriteToFile();
            ReadFromFile("creldes.txt");
            DeleteFile("creldes.txt");
        }
        /*
         * This method creates a new document based on the document given as a parameter, in the format:
         * First line: The user who created the document
         * Second line: The users the document is shared with
         * The rest is the text
         */
        static void WriteToFile()
        {

            TextWriter tw = new StreamWriter("creldes.txt");

            
            tw.Write("Christian Henriksen er altid sej, men også: " + DateTime.Now + " WRITE TO FILE NP");
            

            tw.Close();


        }

        static void ReadFromFile(string fileName) 
        {
            TextReader tr = new StreamReader(fileName);

            Console.Out.WriteLine(tr.ReadLine());

            tr.Close();
 
        }

        /*
         * Deletes the file given the file name
         * TODO:
         * When document class is created it will take the documents title and an extension
         * 
         */
        static void DeleteFile(Document doc)
        {
            string fileName = doc.GetTitle()+".txt";
            
            if (File.Exists(fileName))
            {
                File.Delete(fileName);

            }

        }
    }
}
