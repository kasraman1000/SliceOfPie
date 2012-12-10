using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

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
            doc1.ShareWith(new User("crelde"));
            Document doc2 = new Document("Line1\nLine4\nLine3", "Kewins nye Dokument", new User("Kewin"));

            doc1.MergeWith(doc2, new User("Kewin"));

            Document doc3 = new Document("Line1\nLine4\nLine3\nAnotherLine", "Kewins nye og 3. Dokument", new User("Kewin"));
            doc3.Path = "root\\mappe1";

            doc1.MergeWith(doc3, new User("Kewin"));

            //Console.WriteLine(doc1.Log.ToString());
           
            string docText = "This my new and fabulous blog where i would love to write about my dog called Fuckface!\n" +
                "Fuckface is a really nice dog which sadly only has 3 legs, because one time where i was really angry, and i had this saw, well never mind.\n" +
                "I love my dog above anything else in this world, and i thought i would dedicate this document to him!\n" +
                "This is Fuckface: <img src=\"fuckface.gif\" alt=\"My Dog\">";

            Document testDoc = new Document(docText, "Fuckfacess", new User("Karsten"));
            testDoc.Path = "root\\cuteanimalsxoxo";
            testDoc.ShareWith(new User("ForeverAloneGuy"));
            testDoc.ShareWith(new User("Captain Haddoc"));
            testDoc.ShareWith(new User("Motor-Bjarne"));

            
            WriteToFile(doc1);
            Document loadedDoc = ReadFromFile("8");

            bool textBool = String.Compare(doc1.Text, loadedDoc.Text) == 0;
            bool titleBool = String.Compare(doc1.Title, loadedDoc.Title) == 0;
            bool pathBool = String.Compare(doc1.Path, loadedDoc.Path) == 0;

            bool sharedWithBool = true;

            bool logBool = true;

            foreach (User us in doc1.SharedWith)
            {
                if (!(loadedDoc.SharedWith.Contains(us)))
                {
                    sharedWithBool = false;
                }

            }
            foreach (User us in loadedDoc.SharedWith)
            {
                if (!(doc1.SharedWith.Contains(us)))
                {
                    sharedWithBool = false;
                }
            }


            foreach (Document.DocumentLog.Entry entry in doc1.Log.entries)
            {
                if (!(loadedDoc.Log.entries.Contains(entry)))
                {
                    logBool = false;
                }

            }
            foreach (Document.DocumentLog.Entry entry in loadedDoc.Log.entries)
            {
                if (!(doc1.Log.entries.Contains(entry)))
                {
                    logBool = false;
                }
            }

            List<Document.DocumentLog.Entry> doc1List = doc1.Log.entries;
            List<Document.DocumentLog.Entry> loadedDocList = loadedDoc.Log.entries;

            for (int i = 0; i < doc1List.Count; i++)
            {
                bool derp = doc1List[i].Equals(loadedDocList[i]);
            }




            Console.ReadKey();
            //Console.Write(ReadFromFile("8"));
            /*
           Thread.Sleep(1000);            
           Document retrievedDoc = ReadFromFile(testDoc.Id);
           Console.WriteLine("title: "+retrievedDoc.Title);
           Console.WriteLine("path: " + retrievedDoc.Path);
           Console.WriteLine("owner: " + retrievedDoc.Owner);
           Console.WriteLine("sharedWith: " + retrievedDoc.SharedWith);
           Console.WriteLine("Text: " + retrievedDoc.Text);
           GetHierachy();
           /*
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
            // False means that it will overwrite an existing file with the same id.
            TextWriter tw = new StreamWriter("root\\"+fileName, false);
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
                       
            // And finally it includes the DocumentLog
            tw.WriteLine("");
            tw.Write(doc.Log);

            // Writes the users text into the document
            tw.WriteLine("");
            tw.Write(doc.Text);

            // Closes the writer
            tw.Close();
        }



        public static Document ReadFromFile(string id)
        {
            string fileName = id + ".txt";
           
            try
            {

                // Creates a new reader
                TextReader tr = new StreamReader("root\\"+fileName);

                // Gets the title from the string and 
                string title = tr.ReadLine();

                // Gets the path from the string 
                string path = tr.ReadLine();

                // Gets the name of the owner from the string and makes a user
                string ownerString = tr.ReadLine();
                User owner = new User(ownerString);

                // This part creates the List of users the doc is shared with
                string users = tr.ReadLine();
                string[] userNameArr = users.Split(',');
                List<User> sharedWithList = new List<User>();
                int i = 0;
                foreach (string s in userNameArr)
                {
                    if (!(String.Compare(s,"")==0))
                        sharedWithList.Add(new User(s));
                    i++;
                }

                //KEWIN DU STARTEDE HER
                StringBuilder rest = new StringBuilder();
                // This part creates the Text the document should have
                while (tr.Peek() != -1)
                {
                    rest.AppendLine(tr.ReadLine());
                }
                string restString = rest.ToString();

                string[] restArray = restString.Split(new string[] { "---------------ENDOFLOG------------------" }, StringSplitOptions.None);

                string textString = Regex.Replace(restArray[1], "\r", "");

                string textS = textString.Substring(1);
                string text = textS.Substring(0, textS.Length - 1);


                List<Document.DocumentLog.Entry> entryList = new List<Document.DocumentLog.Entry>();

                string logWithoutMacLineFeed = Regex.Replace(restArray[0], "\r", "");
                string[] entryArray = logWithoutMacLineFeed.Split(new string[] { "Entry" }, StringSplitOptions.None);
               

                for (int index = 1; index < entryArray.Length; index++)
                {
                    List<string> log = new List<string>();
                    string[] entry = entryArray[index].Split(new string[] { "\n" }, StringSplitOptions.None);
                    for (int index2 = 1; index2 < entry.Length; index2++)
                    {
                        if (!(String.Compare(entry[index2],"")==0))
                            log.Add(entry[index2]);
                        }

                    string pattern = @"^\S*";
                    Match m = Regex.Match(log[0],pattern);
                    User user = new User(m.ToString());
                    string stringWithoutOwner = log[0].Substring(m.Index + m.Length);
                    pattern = @"\D*";
                    Match m2 = Regex.Match(stringWithoutOwner, pattern);
                    string desc = m2.ToString().Trim(); 
                    string description = desc.Substring(0,desc.Length-7);
                    string dateString = stringWithoutOwner.Substring(m2.Index + m2.Length);
                    string dateStringWithoutLineFeed = Regex.Replace(dateString, "\n", "");
                    DateTime time = Convert.ToDateTime(dateString);
                    log.RemoveAt(0);

                    entryList.Add(new Document.DocumentLog.Entry(user, description, log, time));

                }
                

                

                // Finally makes the document to return
                Document finalDoc = Document.CreateDocumentFromFile(id, text, title, owner, path, sharedWithList, new Document.DocumentLog(entryList));
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
        public static void DeleteFile(string id)
        {
            // Decides which file the document is associated with
            string fileName = id + ".txt";

            // Checks if a filename that matches the string exists and deletes it
            if(File.Exists("root\\" + fileName))
            {
                File.Delete("root\\" + fileName);
            }
            else
            {
                Console.WriteLine("No file exists by that name");
            }

        }

        public static Folder GetHierachy()
        {
            if(Directory.Exists("root"))
            {
                IEnumerable<string> filesInRoot = Directory.EnumerateFiles("root");
                foreach (string s in filesInRoot)
                {
                    Console.WriteLine(s);
                }


                
                
                








            }



            return null;
        }
    }
}
