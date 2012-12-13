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
    public static class Storage
    {
        //  MAIN FOR TESTING PURPOSES WILL BE REWRITTEN INTO A TESTCLASS!!
        
        public static void Main(string[] args)
        {
            
                        Document doc1 = new Document("Line1\nLine2\nLine3", "Kewins Dokument", new User("Kewin"));

                        Document doc2 = new Document("Line1\nLine4\nLine3", "Kewins nye Dokument", new User("Crelde"));
                        Document doc5 = new Document("Line1\nLine4\nLine3", "Kewins nye Dokument", new User("Derp"));
                        doc1.Path=("cuteanimalsxoxo");
                        doc2.Path=("cuteanimalsxoxo/reptiles");
                        doc5.Path=("cuteanimalsxoxo/reptiles/snakes");

            /*
                        doc1.MergeWith(doc2, new User("Kewin"));
            

                        Document doc3 = new Document("Line1\nLine4\nLine3\nAnotherLine", "Kewins nye og 3. Dokument", new User("Kewin"));
                        doc3.Path = "root";

                        doc1.MergeWith(doc3, new User("Kewin"));

                        //Console.WriteLine(doc1.Log.ToString());
           
                        string docText = "This my new and fabulous blog where i would love to write about my dog called Fuckface!\n" +
                            "Fuckface is a really nice dog which sadly only has 3 legs, because one time where i was really angry, and i had this saw, well never mind.\n" +
                            "I love my dog above anything else in this world, and i thought i would dedicate this document to him!\n" +
                            "This is Fuckface: <img src=\"fuckface.gif\" alt=\"My Dog\">";

                        Document testDoc = new Document(docText, "Fuckfacess", new User("Karsten"));
                        testDoc.Path = "root\\cuteanimalsxoxo";
             * 
                        List<User> sh = new List<User>();
                        sh.Add(new User("ForeverAloneGuy"));
                        sh.Add(new User("Captain Haddoc"));
                        sh.Add(new User("Motor-Bjarne"));

                        Project proj = new Project("Creldes project", new User("Crelde"), sh);
                        SaveProjectToFile(proj);
                        WriteToFile(proj, doc1);
                        WriteToFile(proj, doc5);
                        WriteToFile(proj, doc2);
                        GetHierachy(proj.Id);
            */
                        GetAllProjects();
            //testDoc.ShareWith(new User("ForeverAloneGuy"));
                       // testDoc.ShareWith(new User("Captain Haddoc"));
                       // testDoc.ShareWith(new User("Motor-Bjarne"));
                        
           // GetHierachy();
            /*
            Document documentWithEmptyText = new Document("", "Document", new User("kewin"));
             WriteToFile(doc1);
             Document loadedDoc = ReadFromFile("8");

             bool textBool = String.Compare(doc1.Text, loadedDoc.Text) == 0;
             bool titleBool = String.Compare(doc1.Title, loadedDoc.Title) == 0;
             bool pathBool = String.Compare(doc1.Path, loadedDoc.Path) == 0;


             bool logBool = true;

            


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
        
        
        public static void WriteToFile(Project pro, Document doc)
        {
            // Creates a fileName for the file based on the files title
            string fileName = pro.Id+"\\"+doc.Id + ".txt";

            // False means that it will overwrite an existing file with the same id.
            TextWriter tw = new StreamWriter(fileName, false);
            // Writes the first line in the document file which should be the title
            tw.WriteLine(doc.Title);

            // Writes the second line in the document which should be the path (for gui representation)
            tw.WriteLine(doc.Path);

            // Writes the third line in the file which is the owner of the document
            tw.WriteLine(doc.Owner.ToString());
                                  
            // Write the documents log
            tw.WriteLine("");
            tw.Write(doc.Log);

            // Writes the users text into the document
            tw.WriteLine("");
            tw.Write(doc.Text);

            // Closes the writer
            tw.Close();
        }

        public static void SaveProjectToFile(Project p)
        {
            if (!Directory.Exists(p.Id))
            {
                Directory.CreateDirectory(p.Id);
                TextWriter tw = new StreamWriter(p.Id+"\\MetaInfo.txt", false);
                tw.WriteLine(p.Title);
                tw.WriteLine(p.Owner.ToString());

                List<User> userList = p.SharedWith;
                User[] users = userList.ToArray();
                string[] userNames = new string[users.Length];
                
                int i = 0;
                foreach (User u in users)
                {
                    userNames[i] = u.ToString();
                    i++;
                }
                StringBuilder sb = new StringBuilder();
                int j = 1;
                foreach (string s in userNames)
                {
                    if (!(j==userNames.Length))
                    {
                        sb.AppendFormat(s+", ");
                    }
                    else
                    {
                        sb.AppendFormat(s);
                    }
                    j++;
                }
                tw.WriteLine(sb.ToString());
                tw.Close();

            }

        }



        public static Document ReadFromFile(string fid, string did)
        {
            string fileName = fid+"\\"+ did + ".txt";
           
            try
            {

                // Creates a new reader
                TextReader tr = new StreamReader(fileName);

                // Gets the title from the string and 
                string title = tr.ReadLine();

                // Gets the path from the string 
                string path = tr.ReadLine();

                // Gets the name of the owner from the string and makes a user
                string ownerString = tr.ReadLine();
                User owner = new User(ownerString);

                // Create a StringBilder and append the rest of the file to it, it will contain both the log and the text of the document.
                StringBuilder rest = new StringBuilder();
                while (tr.Peek() != -1)
                {
                    rest.AppendLine(tr.ReadLine());
                }
                string restString = rest.ToString();

                // Splitting on the string "---------------ENDOFLOG------------------", by this we assume the user will NEVER write this exact
                // string in his document, if he does, our program will not work.
                string[] restArray = restString.Split(new string[] { "---------------ENDOFLOG------------------" }, StringSplitOptions.None);

                if (restArray.Length!=2)
                    throw new Exception("The user wrote the string \"---------------ENDOFLOG------------------\", as part of his text");

                // Remove the Mac line feed character from the text string.
                string textString = Regex.Replace(restArray[1], "\r", "");
                // Remove normal line feed at start of text.
                string textS = textString.Substring(1);
                // Remove normal line feed at end of text.
                string text = "";
                if (textS.Length!=0)
                    text = textS.Substring(0, textS.Length - 1);

                // The list of entries the Document's Log will receive
                List<Document.DocumentLog.Entry> entryList = new List<Document.DocumentLog.Entry>();
                // Remove Mac line feed from Log.
                string logWithoutMacLineFeed = Regex.Replace(restArray[0], "\r", "");
                // Split the log into separate strings for each Entry.
                string[] entryArray = logWithoutMacLineFeed.Split(new string[] { "Entry" }, StringSplitOptions.None);
               
                // This loop takes each string in entryArray ( excluding the first, which is just "Log:" ), and creates an Entry from it.
                for (int index = 1; index < entryArray.Length; index++)
                {
                    // Create the Entrys log ( first line being the description, which will be removed before the Entry is created).
                    List<string> log = new List<string>();
                    string[] entry = entryArray[index].Split(new string[] { "\n" }, StringSplitOptions.None);
                    for (int index2 = 1; index2 < entry.Length; index2++)
                    {
                        if (!(String.Compare(entry[index2],"")==0))
                            log.Add(entry[index2]);
                        }
                    // Find the first word in the description, which is the Users name.
                    string pattern = @"^\S*";
                    Match m = Regex.Match(log[0],pattern);
                    // Create the User object.
                    User user = new User(m.ToString());
                    // Get all remaining characters untill a number is found ( this being the description ).
                    string stringWithoutOwner = log[0].Substring(m.Index + m.Length);
                    pattern = @"\D*";
                    Match m2 = Regex.Match(stringWithoutOwner, pattern);
                    // Trim for whitespaces at start and end of string.
                    string desc = m2.ToString().Trim(); 
                    // Remove last 7 characters in string ( this part of the string is auto generated by the log, and not actually
                    // part of the description.
                    string description = desc.Substring(0,desc.Length-7);
                    // Finally get the last part of the string, which is the DateTime of the Entry.
                    string dateString = stringWithoutOwner.Substring(m2.Index + m2.Length);
                    // Remove line feed from date string.
                    string dateStringWithoutLineFeed = Regex.Replace(dateString, "\n", "");
                    // Convert date string into DateTime object.
                    DateTime time = Convert.ToDateTime(dateString);
                    // As mentioned above, first entry in the log was the description, and that should not be part
                    // of the log, so it is removed.
                    log.RemoveAt(0);
                    // Created the Entry object, and add it to the entryList.
                    entryList.Add(new Document.DocumentLog.Entry(user, description, log, time));

                }
                

                

                // Finally makes the document to return
                Document finalDoc = Document.CreateDocumentFromFile(did, text, title, owner, path, new Document.DocumentLog(entryList));
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
        public static void DeleteFile(string pid, string did)
        {
            // Decides which file the document is associated with
            string fileName = pid+"\\"+did + ".txt";

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

        public static Project GetHierachy(string pid)
        {
            if(Directory.Exists(pid))
            {
                IEnumerable<string> distinctFolderNames;
                List<Folder> folders = new List<Folder>();
                List<string> toBeFolders = new List<string>();
                IEnumerable<string> filesInRoot = Directory.EnumerateFiles(pid);
                List<DocumentStruct> structs = new List<DocumentStruct>();

                foreach (string s in filesInRoot)
                {
                    
                    TextReader tr = new StreamReader(s);

                    string title = tr.ReadLine();
                    string path = tr.ReadLine();
                    User user = new User(tr.ReadLine());
               
                    string[] filePath = path.Split('/');
                    
                    foreach (string st in filePath)
                    {
                        toBeFolders.Add(st);
                        
                    }
                   
                    tr.Close();

                    string id = Path.GetFileNameWithoutExtension(s);

                    structs.Add(new DocumentStruct(title, user, id, path));  
                    
        
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
                            
                            if(!splitPath[i-1].Equals(splitPath[0]))
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
                if (folders.Count == 0)
                {
                    Console.WriteLine("There is no folders to show");

                }
                else
                {
                    Folder finalFolder = folders.ElementAt(0);
                

                
                TextReader mr = new StreamReader(pid+"\\MetaInfo.txt");
                string ti = mr.ReadLine();
                User us = new User(mr.ReadLine());
                List<User> sha = new List<User>();
                string[] userNames = (mr.ReadLine().Split(','));
                foreach (string str in userNames)
                {
                    sha.Add(new User(str.Trim()));
                }

                Project finalProject = new Project(ti, us, sha);
                finalProject.AddChild(finalFolder);
                return finalProject;
                }

            }

            Console.WriteLine("No Project exists by that id");
            return null;

        }


        public static List<Project> GetAllProjects()
        {
            string creldesPath = "\\Users\\Crelde\\git\\SliceOfPie\\SliceOfPie\\SliceOfPie\\bin\\Debug";
            string kasraPath = "??";
            List<Project> projs = new List<Project>();

            IEnumerable<string> projects = Directory.EnumerateDirectories(creldesPath);

            foreach (String p in projects)
            {
                projs.Add(GetHierachy(p));
            }

            return projs;
        }
    }
}
