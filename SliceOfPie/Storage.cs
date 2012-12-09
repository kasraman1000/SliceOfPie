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
    static class Storage
    {
        //  MAIN FOR TESTING PURPOSES WILL BE REWRITTEN INTO A TESTCLASS!!
        
        public static void Main(string[] args)
        {
            Document doc1 = new Document("Line1\nLine2\nLine3", "Kewins Dokument", new User("Kewin"));
            Document doc2 = new Document("Line1\nLine4\nLine3", "Kewins nye Dokument", new User("Kewin"));

            doc1.MergeWith(doc2, new User("Kewin"));

            Document doc3 = new Document("Line1\nLine4\nLine3\nAnotherLine", "Kewins nye og 3. Dokument", new User("Kewin"));
            doc3.Path = "root\\mappe1";

            doc1.MergeWith(doc3, new User("Kewin"));

            Console.WriteLine(doc1.Log.ToString());
            /*
            string docText = "This my new and fabulous blog where i would love to write about my dog called Fuckface!\n" +
                "Fuckface is a really nice dog which sadly only has 3 legs, because one time where i was really angry, and i had this saw, well never mind.\n" +
                "I love my dog above anything else in this world, and i thought i would dedicate this document to him!\n" +
                "This is Fuckface: <img src=\"fuckface.gif\" alt=\"My Dog\">";

            Document testDoc = new Document(docText, "Fuckfacess", new User("Karsten"));
            testDoc.ShareWith(new User("ForeverAloneGuy"));
            testDoc.ShareWith(new User("Captain Haddoc"));
            testDoc.ShareWith(new User("Motor-Bjarne"));

            
            WriteToFile(testDoc);
            */
            /*
            Document retrievedDoc = ReadFromFile("Fuckface");
            string[] txt = retrievedDoc.CreateTextArray();
           
            for (int i = 0; i < txt.Length; i++)
            {
                Console.Out.WriteLine("array[" + i + "] : " + txt[i]);
            }
            DeleteFile("Fuckface");
            

            Folder fold = new Folder("herpderps");
            Folder anotherFolder = new Folder("FolderCeption");
            Folder thirdFolder= new Folder("FolderCeptionEVENMOAR");
            fold.AddChild(testDoc);
            
            Document anotherDoc = new Document("Hello im just another doc","CreldeDoc", new User("Crelde"));
            anotherFolder.AddChild(anotherDoc);
            anotherFolder.AddChild(thirdFolder);
            fold.AddChild(anotherFolder);
            */
        }


        /*
         * This method creates a new document based on the document given as a parameter, in the format:
         * First line: The user who created the document
         * Second line: The users the document is shared with
         * The rest is the text
         */

        // Second parameter is optional for now, chooses where to put the file
        
        
        public static void WriteToFile(Document doc)
        {
            // Creates a fileName for the file based on the files title
            string fileName = doc.Id + ".txt";

            // Create the root folder if it doesnt exist
            if (!Directory.Exists("root"))
            {
                Directory.CreateDirectory("root");

            }

            TextWriter tw = new StreamWriter("root\\"+fileName);
            // Writes the first line in the document file which should be the title
            tw.WriteLine(doc.Title);

            // Writes the second line in the document which should be the path (for gui representation)
            tw.WriteLine(doc.Path);

            // Writes the third line in the file which is the owner of the document
            tw.WriteLine(doc.Owner.ToString());
           
            // Makes the array with usernames that the document is shared with ready 
            List<User> sharedwith = doc.SharedWith;
            User[] userArray = sharedwith.ToArray();
            String[] userNames = new string[userArray.Length];

            int i = 0;
            foreach (User u in userArray)
            {
                userNames[i] = u.ToString();
                i++;
            }

            // Writes the fourth line, which is the usernames that the document is shared with
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

            // Writes the users text into the document
            tw.WriteLine("");
            tw.Write(doc.Text);

            // And finally it includes the DocumentLog
            tw.WriteLine("");
            tw.Write(doc.Log.ToString());

            // Closes the writer
            tw.Close();
        }

        public static Document ReadFromFile(string id)
        {
            string fileName = id + ".txt";
           
            try
            {

                // Creates a new reader
                TextReader tr = new StreamReader(fileName);

                // Gets the name of the owner from the string and makes a user
                string ownerString = tr.ReadLine();
                User owner = new User(ownerString);

                // This part creates the List of users the doc is shared with
                string users = tr.ReadLine();
                string[] userNameArr = users.Split(',');
                User[] userArr = new User[userNameArr.Length];
                int i = 0;
                foreach (string s in userNameArr)
                {
                    userArr[i] = new User(s);
                    i++;
                }
                List<User> userList = userArr.ToList<User>();

                StringBuilder text = new StringBuilder();
                // This part creates the Text the document should have
                while (tr.Peek() != -1)
                {
                    text.AppendLine(tr.ReadLine());
                }
                string finalText = text.ToString();

                // Finally makes the document to return
                Document finalDoc = new Document(finalText, id, owner, userList);
 
                // Closes the reader
                tr.Close();
                return finalDoc;
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("No file exists by that name");
                return null;
            }

        }

        /*
         * Deletes the file given the file name 
         */
        public static void DeleteFile(string title)
        {
            // Decides which file the document is associated with
            string fileName = title + ".txt";

            // Checks if a filename that matches the string exists and deletes it
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            else
            {
                Console.WriteLine("No file exists by that name");
            }

        }
    }
}
