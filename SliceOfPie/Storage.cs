﻿using System;
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
            
            string docText = "This my new and fabulous blog where i would love to write about my dog called Fuckface!\n" +
                "Fuckface is a really nice dog which sadly only has 3 legs, because one time where i was really angry, and i had this saw, well never mind.\n" +
                "I love my dog above anything else in this world, and i thought i would dedicate this document to him!\n" +
                "This is Fuckface: <img src=\"fuckface.gif\" alt=\"My Dog\">";

            Document testDoc = new Document(docText, "Fuckfacess", new User("Karsten"));
            testDoc.ShareWith(new User("ForeverAloneGuy"));
            testDoc.ShareWith(new User("Captain Haddoc"));
            testDoc.ShareWith(new User("Motor-Bjarne"));

            
            /*
            WriteToFile(testDoc);

            Document retrievedDoc = ReadFromFile("Fuckface");
            string[] txt = retrievedDoc.CreateTextArray();
           
            for (int i = 0; i < txt.Length; i++)
            {
                Console.Out.WriteLine("array[" + i + "] : " + txt[i]);
            }
            DeleteFile("Fuckface");

            */
            Folder fold = new Folder("herpderps");
            Folder anotherFolder = new Folder("FolderCeption");
            fold.AddChild(testDoc);
            
            Document anotherDoc = new Document("Hello im just another doc","CreldeDoc", new User("Crelde"));
            anotherFolder.AddChild(anotherDoc);
            fold.AddChild(anotherFolder);
            CreateNewFolder(fold);
        }


        /*
         * This method creates a new document based on the document given as a parameter, in the format:
         * First line: The user who created the document
         * Second line: The users the document is shared with
         * The rest is the text
         */

        // Second parameter is optional for now, chooses where to put the file
        public static void WriteToFile(Document doc, string filePath = "")
        {
            // Creates a fileName for the file based on the files title
            string fileName = doc.Title + ".txt";
            TextWriter tw = new StreamWriter(filePath+fileName);

            // Writes the first line in the file which is the owner of the document
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
            tw.Write(doc.Text);

            // Closes the writer
            tw.Close();
        }

        public static void CreateRootFolder()
        {
            if (!File.Exists("root"))
            {
                Directory.CreateDirectory("C:\\root");
            }

        }
        // OOOKAY.. functionality finally works.. mostly for testing fordi det skal omskrives til flere metoder det her..
        // MEN det kan tage imod en folder med en folder i med et dokument i og lave hele hierakiet i filesystem på c//
        // Det scaler bare ikke endnu
        public static void CreateNewFolder(Folder f)
        {
            // In case there isn't a root folder yet, it makes one
            CreateRootFolder();


            List<IFileSystemComponent> children = f.GetChildren();

            // Assuming the folder has no other parents than the root
            Directory.CreateDirectory("C:\\root\\" + f.Title);
                           
                foreach(IFileSystemComponent file in children)
                {
                    // Should make some function thingy that makes up the proper filepath
                    // Right now it puts the document into the folder given as a parameter to the method
                    string path = "C:\\root\\"+f.Title+"\\";

                    // Checks if the given file has any documents and writes to the right folder
                    if (file.FileType == DocType.Document)
                    {
                        WriteToFile((Document)file, path);
                    }
                    else if (file.FileType == DocType.Folder)
                    {
                        Directory.CreateDirectory("C:\\root\\" + f.Title+"\\"+ file.Title);
                        List<IFileSystemComponent> files = ((Folder)file).GetChildren();
                        if(files.Count != 0)
                        {
                            foreach (IFileSystemComponent ifiles in files)
                            {
                                path = "C:\\root\\" + f.Title+"\\"+ file.Title+"\\";
                                if (ifiles.FileType == DocType.Document)
                                {
                                    WriteToFile((Document)ifiles, path);
                                }
                            }
                                            
                        }
                        
                    }
                }

        }


        public static Document ReadFromFile(string title)
        {
            string fileName = title + ".txt";
           
            try
            {
                // Decides which file the document is associated with


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
                Document finalDoc = new Document(finalText, title, owner, userList);
 
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
