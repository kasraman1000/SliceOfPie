using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace SliceOfPie
{
    /*
     * This class is responsible for everything that has to do with our external storage this being the files the users are saving.
     */
    public static class Storage
    {
        //  MAIN FOR TESTING PURPOSES WILL BE REWRITTEN INTO A TESTCLASS!!
        
        public static void Main(string[] args)
        {
            /*
             * 
            Document doc1 = new Document("Line1\nLine2\nLine3", "Kewins Dokument", new User("Crelde"));
            Document doc2 = new Document("Line1\nLine4\nLine3", "Kewins nye Dokument", new User("Kewin"));

            doc1.MergeWith(doc2, new User("Kewin"));
            

            Document doc3 = new Document("Line1\nLine4\nLine3\nAnotherLine", "Kewins nye og 3. Dokument", new User("Kewin"));
            doc3.Path = "root/mappe1/insideJoke";

            doc1.MergeWith(doc3, new User("Kewin"));

           // Console.WriteLine(doc1.Log.ToString());
           
            string docText = "This my new and fabulous blog where i would love to write about my dog called Fuckface!\n" +
                "Fuckface is a really nice dog which sadly only has 3 legs, because one time where i was really angry, and i had this saw, well never mind.\n" +
                "I love my dog above anything else in this world, and i thought i would dedicate this document to him!\n" +
                "This is Fuckface: <img src=\"fuckface.gif\" alt=\"My Dog\">";

            Document testDoc = new Document(docText, "Fuckfacess", new User("Karsten"));
            testDoc.Path = "root\\cuteanimalsxoxo";
            testDoc.ShareWith(new User("ForeverAloneGuy"));
            testDoc.ShareWith(new User("Captain Haddoc"));
            testDoc.ShareWith(new User("Motor-Bjarne"));
            */
            GetHierachy();
           
            //WriteToFile(doc1);
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

            // Writes the users text into the document
            tw.WriteLine("");
            tw.Write(doc.Text);

            // And finally it includes the DocumentLog
            tw.WriteLine("");
            tw.Write(doc.Log);

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
                Document finalDoc = new Document( finalText, title, owner, userList);
                finalDoc.Path = path; 
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
                IEnumerable<string> distinctFolderNames;
                List<Folder> folders = new List<Folder>();
                List<string> toBeFolders = new List<string>();
                IEnumerable<string> filesInRoot = Directory.EnumerateFiles("root");
                List<DocumentStruct> structs = new List<DocumentStruct>();

                foreach (string s in filesInRoot)
                {
                    
                    TextReader tr = new StreamReader(s);

                    string title = tr.ReadLine();
                    string path = tr.ReadLine();
                    User user = new User(tr.ReadLine());
                    // Make sharedWith list
                    string users = tr.ReadLine();
                    string[] userNameArr = users.Split(',');
                    User[] userArr = new User[userNameArr.Length];
                    int i = 0;
                    foreach (string u in userNameArr)
                    {
                        userArr[i] = new User(u);
                        i++;
                    }
                    List<User> userList = userArr.ToList<User>();

                    string[] filePath = path.Split('/');
                    
                    foreach (string st in filePath)
                    {
                        toBeFolders.Add(st);
                        
                    }
                   
                    tr.Close();
                    structs.Add(new DocumentStruct(title, user, s, path, userList));  
                    
        
                }
                distinctFolderNames = toBeFolders.Distinct();
                foreach (string folderName in distinctFolderNames)
                {
                    folders.Add(new Folder(folderName));
                }

                foreach (DocumentStruct d in structs)
                {
                    string[] folder = d.Path.Split('/');
                    foreach(Folder fo in folders)
                    {
                        if (fo.Title.Equals(folder.Last()))
                        {
                            fo.AddChild(d);
                        }
                    }
                }

                foreach (string s in filesInRoot)
                {
                    TextReader tr = new StreamReader(s);
                    tr.ReadLine();
                    string path = tr.ReadLine();
                    string[] splitPath = path.Split('/');
                    for (int i = splitPath.Length; i != 0; i--)
                    {
                        
                        foreach (Folder fol in folders)
                        {
                            
                            if(!splitPath[i-1].Equals("root"))
                            {
                                
                               if (fol.Title.Equals(splitPath[i-1]))
                               {
                                   
                                 var r1 = from f in folders
                                            where f.Title.Equals(splitPath[i-1])
                                            select f;

                                 var r2 = from f in folders
                                         where f.Title.Equals(splitPath[i-2])
                                         select f;

                                 List<IFileSystemComponent> derp1 = r2.FirstOrDefault().Children;
                                       Folder derp3 = r1.FirstOrDefault();
                                 if (!derp1.Contains(derp3) && !derp1.Equals(derp3))
                                 {
                                     r2.FirstOrDefault().AddChild(r1.FirstOrDefault());
                                 }
                                    
                                }
                            }
                            
                        }
                      
                    }
                }
                Folder finalFolder = folders.ElementAt(0);

            }



            return null;
        }
    }
}
