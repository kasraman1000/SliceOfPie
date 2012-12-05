using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SliceOfPie
{
    /*
     * This class is responsible for everything that has to do with our external storage this being the files the users are saving.
     */
    class Storage
    {
        //  MAIN FOR TESTING PURPOSES WILL BE REWRITTEN INTO A TESTCLASS!!
        
        public static void Main(string[] args)
        {
            string docText = "This my new and fabulous blog where i would love to write about my dog called Fuckface!\n" +
                "Fuckface is a really nice dog which sadly only has 3 legs, because one time where i was really angry, and i had this saw, well never mind.\n" +
                "I love my dog above anything else in this world, and i thought i would dedicate this document to him!\n" +
                "This is Fuckface: <img src=\"fuckface.gif\" alt=\"My Dog\">" ;

            Document testDoc = new Document(docText, "Fuckface", new User("Karsten", 1338));
            testDoc.ShareWith(new User("ForeverAloneGuy", 8999));
            testDoc.ShareWith(new User("Captain Haddoc", 666));
            testDoc.ShareWith(new User("Motor-Bjarne", 24));


            WriteToFile(testDoc);
            ReadFromFile(testDoc);
           // DeleteFile(testDoc);
        }
        

        /*
         * This method creates a new document based on the document given as a parameter, in the format:
         * First line: The user who created the document
         * Second line: The users the document is shared with
         * The rest is the text
         */
        public static void WriteToFile(Document doc)
        {
            // Creates a fileName for the file based on the files title
            string fileName = doc.GetTitle() + ".txt";
            TextWriter tw = new StreamWriter(fileName);
            // Writes the first line in the file which is the owner of the document
            tw.WriteLine(doc.GetOwner().GetName());
            // Makes the array with usernames that the document is shared with ready 
            List<User> sharedwith = doc.GetSharedWith();
            User[] userArray = sharedwith.ToArray();
            String[] userNames = new string[userArray.Length];
            
            int i = 0;
            foreach (User u in userArray)
            { 
                userNames[i] = u.GetName();
                i++;
            }

            // Writes the second line, which is the usernames that the document is shared with
            int j = 1;
            foreach (string s in userNames)
            {
                // If its the last username it doesn't write a comma
                if (j == userNames.Length)
                {
                    tw.Write(s);
                }

                else
                {
                    tw.Write(s + ", ");
                }

                j++;
            }
            // Finally writes the users text into the document
            tw.WriteLine("");            
            tw.Write(doc.GetText());
            // Closes the writer
            tw.Close();
        }


        public static void ReadFromFile(Document doc) 
        {
            // Decides which file the document is associated with
            string fileName = doc.GetTitle() + ".txt";

            // Creates a new reader and as long as we haven't reached end of file it continues to output the lines.
            TextReader tr = new StreamReader(fileName);
            while (tr.Peek() != -1)
            {
                Console.Out.WriteLine(tr.ReadLine());
            }
            // Closes the reader
            tr.Close();
 
        }

        /*
         * Deletes the file given the file name 
         */
        public static void DeleteFile(Document doc)
        {
            // Decides which file the document is associated with
            string fileName = doc.GetTitle()+".txt";
            // Checks if a filename that matches the string exists and deletes it
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

        }
    }
}
