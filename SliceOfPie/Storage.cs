using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Diagnostics;

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
            string path = pro.Id;
            string fileName;

            fileName = path + "\\" + doc.Id + ".txt";
            

            // False means that it will overwrite an existing file with the same id.
            using (TextWriter tw = new StreamWriter(fileName, false))
            {
                // Writes the first line in the document file which should be the title
                tw.WriteLine(doc.Title);

                // Writes the second line in the document which should be the path (for gui representation)
                tw.WriteLine(doc.Path);

                // Writes the third line in the file which is the owner of the document
                tw.WriteLine(doc.Owner.ToString());

                // Writes the 4th line, which is the pictures that are attached to the docmuent.
                // If the pictures are not saved already, save them.
                IEnumerable<string> filesInRoot = Directory.EnumerateFiles(path);
                StringBuilder imageLineBuilder = new StringBuilder();
                for (int i = 0; i < doc.Images.Count; i++)
                {
                    string picPath = pro.Id + @"\" + doc.Images[i].Id + ".JPG";
                    string currentDir = Directory.GetCurrentDirectory();
                    if (!(filesInRoot.Contains(picPath)))
                        SavePictureToFile(doc.Images[i], currentDir + @"\" + picPath);
                    if (i == doc.Images.Count - 1)
                        imageLineBuilder.AppendFormat(doc.Images[i].Id);
                    else
                        imageLineBuilder.AppendFormat(doc.Images[i].Id + ",");
                }
               
                tw.WriteLine(imageLineBuilder.ToString());

                // Write the modified boolean
                tw.WriteLine(doc.Modified.ToString());

                // Write the deleted boolean
                tw.WriteLine(doc.Deleted.ToString());

                // Write the documents log
                tw.WriteLine("");
                tw.Write(doc.Log);

                // Writes the users text into the document
                tw.WriteLine("");
                tw.Write(doc.Text);
            }
        }

        private static void SavePictureToFile(Picture pic, string path)
        {            
            try
            {
                // Create a Bitmap object from the image, and save that in the projects folder.
                Bitmap b = new Bitmap(pic.Image);
                b.Save(path);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not save Image");
            }
        }

        public static void DeletePicture(string projectId, Picture picture)
        {
            string pictureId = picture.Id;
            picture.Image.Dispose();
            string fileName = projectId + "\\" + pictureId + ".JPG";

            // Checks if a filename exists, if it does, delete it.
            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (IOException)
                {
                    Debug.Print("Could not Delete picture named " + fileName + " because the program could not access the file.");
                }
            }
            else
            {
                Console.WriteLine("No file exists by that name");
            }
        }

        // Saves a project to the file system if it doesnt already exist.
        // If it is already there, update its MetaInfo file.
        public static void SaveProjectToFile(Project p)
        {
            // The path the project should be saved to.
            string path = p.Id;
           
            // Check if the project exists, if it doesnt, save it.
            if (!Directory.Exists(p.Id))
            {
                // Create the MetaInfo file, which contains the Title of the project,
                // the owner and the users the project is shared with.
                using (TextWriter tw = new StreamWriter(path + "\\MetaInfo.txt", false))
                {
                    Directory.CreateDirectory(path);
                    // Write title
                    tw.WriteLine(p.Title);
                    // Write owner.
                    tw.WriteLine(p.Owner.ToString());
                    // Write users the project is ahred with
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
                        if (!(j == userNames.Length))
                        {
                            sb.AppendFormat(s + ", ");
                        }
                        else
                        {
                            sb.AppendFormat(s);
                        }
                        j++;
                    }
                    tw.WriteLine(sb.ToString());
                }
            }
            else
            {
                // If the project already exists, overwrite the MetaInfoFile with the new information.
                using (TextWriter tw = new StreamWriter(path, false))
                {
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
                        if (!(j == userNames.Length))
                        {
                            sb.AppendFormat(s + ", ");
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
        }

        public static void DeleteProject(string projectId)
        {
            // Path of the project
            string path = projectId;
            
            // If it exists, delete it.
            if (Directory.Exists(path))
                Directory.Delete(path,true);
        }

        public static Document ReadFromFile(string pid, string did)
        {
            string fileName = pid + "\\" + did + ".txt";
            Document finalDoc;

            try
            {
                // Creates a new reader
                StreamReader sr = new StreamReader(fileName);
                using (TextReader tr = sr)
                {
                    // Gets the title from the string and 
                    string title = tr.ReadLine();

                    // Gets the path from the string 
                    string path = tr.ReadLine();

                    // Gets the name of the owner from the string and makes a user
                    string ownerString = tr.ReadLine();
                    User owner = new User(ownerString);
                    
                    // Gets the images id from the file, and add them as picture elements to the list of pictures.
                    string imageString = tr.ReadLine();
                    string[] imageArray = imageString.Split(new string[] { "," }, StringSplitOptions.None);

                    List<Picture> pictures = new List<Picture>();
                    foreach (string s in imageArray)
                    {
                        if ((String.Compare(s,"")!=0))
                        {
                            string dir = Directory.GetCurrentDirectory();
                            string pathToPicture = dir + @"\" + pid + @"\" + s + ".JPG"; 
                            Bitmap picture = new Bitmap(pathToPicture);
                            pictures.Add(new Picture(picture,s));
                        }
                    }
                    
                    // Reading the modified and deleted booleans 
                    bool modified = Boolean.Parse(tr.ReadLine());
                    bool deleted = Boolean.Parse(tr.ReadLine());



                    // Create a StringBuilder and append the rest of the file to it, it will contain both the log and the text of the document.
                    StringBuilder rest = new StringBuilder();
                    while (tr.Peek() != -1)
                    {
                        rest.AppendLine(tr.ReadLine());
                    }
                    string restString = rest.ToString();

                    // Splitting on the string "---------------ENDOFLOG------------------", by this we assume the user will NEVER write this exact
                    // string in his document, if he does, our program will not work.
                    string[] restArray = restString.Split(new string[] { "---------------ENDOFLOG------------------" }, StringSplitOptions.None);

                    if (restArray.Length != 2)
                        throw new Exception("The user wrote the string \"---------------ENDOFLOG------------------\", as part of his text");

                    // Remove the Mac line feed character from the text string.
                    string textString = Regex.Replace(restArray[1], "\r", "");
                    // Remove normal line feed at start of text.
                    string textS = textString.Substring(1);
                    // Remove normal line feed at end of text.
                    string text = "";
                    if (textS.Length != 0)
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
                            if (!(String.Compare(entry[index2], "") == 0))
                                log.Add(entry[index2]);
                        }
                        // Find the first word in the description, which is the Users name.
                        string pattern = @"^\S*";
                        Match m = Regex.Match(log[0], pattern);
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
                        string description = desc.Substring(0, desc.Length - 7);
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


                    tr.ReadToEnd();
                    sr.DiscardBufferedData();
                    tr.Dispose();
                    // Finally makes the document to return
                    finalDoc = Document.CreateDocumentFromFile(did, text, title, modified, deleted, owner, pictures, path, new Document.DocumentLog(entryList));
                }
                //sr.ReadToEnd();
                sr.Dispose();
                sr.Close();
                
                object o = sr.GetLifetimeService();
                return finalDoc;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No file exists by that name");
                return null;
            }

        }

        /*
         * Deletes the file given the file name 
         */
        public static void DeleteDocument(string pid, string did)
        {
            string fileName = pid + "\\" + did + ".txt";
            
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
            // Path of the project
            string folderPath = pid;
            
            if(Directory.Exists(folderPath))
            {
                IEnumerable<string> distinctFolderNames;
                List<Folder> folders = new List<Folder>();
                List<string> potentialFoldersInRoot = new List<string>();
                List<string> toBeFolders = new List<string>();
                // Get an Enumerable of all the files in the Project.
                IEnumerable<string> filesInRoot = Directory.EnumerateFiles(folderPath);
                List<DocumentStruct> structs = new List<DocumentStruct>();

                foreach (string s in filesInRoot)
                {
                    // If the file is not the MetaInfo file, or an image, it's a document
                    // that should be made as a DocumentStruct.
                    if (!(s.Contains("MetaInfo.txt")) && (!(s.Contains(".JPG"))))
                    {
                        using (TextReader tr = new StreamReader(s))
                        {
                            // Read the title
                            string title = tr.ReadLine();
                            // Read the Path
                            string path = tr.ReadLine();
                            // Read the User
                            User user = new User(tr.ReadLine());
                            // Add all folders the path contains to the list of toBeFolders.
                            string[] filePath = path.Split('/');
                            foreach (string st in filePath)
                            {
                                toBeFolders.Add(st);

                            }
                            // Get the files name, which is the documents id.
                            string id = Path.GetFileNameWithoutExtension(s);
                            // Add the struct to the list of structs.
                            structs.Add(new DocumentStruct(title, user, id, path));
                        }
                    }

                }
                // Of all the folders added to the toBeFolders, get each distinct one, and create
                // a Folder object by that name.
                distinctFolderNames = toBeFolders.Distinct();
                foreach (string folderName in distinctFolderNames)
                {
                    folders.Add(new Folder(folderName));
                    // Add it as a potential folder in root of the project.
                    potentialFoldersInRoot.Add(folderName);
                }

                foreach (DocumentStruct d in structs)
                {
                    // Figure out which folder the struct should be in.
                    string[] folder = d.Path.Split('/');
                    foreach (Folder fo in folders)
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
                                     Folder fold = (Folder)r1.FirstOrDefault();
                                     // Remove the folder from the list of potential root folders, as it was added
                                     // to another folder.
                                     potentialFoldersInRoot.Remove(fold.ToString());
                                 }
                                    
                                }
                            }
                            
                        }
                        tr.Close();
                        tr.Dispose();
                    }
                }
                if (folders.Count == 0)
                {
                    Console.WriteLine("There is no folders to show");

                }
                else
                {
                    // Read the info from the MetaInfo file.
                    TextReader mr = new StreamReader(folderPath + "\\MetaInfo.txt");
                    // Read title.
                    string ti = mr.ReadLine();
                    // Read owner.
                    User us = new User(mr.ReadLine());
                    // Read list of users the project is shared with.
                    List<User> sha = new List<User>();
                    string[] userNames = (mr.ReadLine().Split(','));
                    foreach (string str in userNames)
                    {
                        sha.Add(new User(str.Trim()));
                    }
                    // Create the project object with the paramerters read.
                    Project finalProject = new Project(ti, us, sha, Path.GetFileNameWithoutExtension(folderPath));

                    // Add all remaining folders to root of project, and add all
                    // structs with "" as their folder to root as well.
                    foreach (Folder fol in folders)
                    {
                        if (potentialFoldersInRoot.Contains(fol.ToString()))
                        {
                            if (String.Compare(fol.Title, "") == 0)
                            {
                                // Add the documentstruct.
                                foreach (DocumentStruct docStruct in fol.Children)
                                    finalProject.AddChild(docStruct);

                            }
                            // Add the folder.
                            else
                                finalProject.AddChild(fol);
                        }
                    }
    
                    mr.Close();
                    mr.Dispose();
                    return finalProject;
                }
            }

            Console.WriteLine("No Project exists by that id");
            return null;
        }

        public static List<Project> GetAllProjects()
        {
            List<Project> projs = new List<Project>();
            // The path to look for projects (directories)
            string path = Directory.GetCurrentDirectory();
            
            // Enumerate all directories, which infact are projects.
            IEnumerable<string> projects = Directory.EnumerateDirectories(path);
            // For each project, get its hierachy, and add it to the list of projects.
            foreach (String p in projects)
            {
                projs.Add(GetHierachy(p));
            }

            return projs;
        }
    }
}
