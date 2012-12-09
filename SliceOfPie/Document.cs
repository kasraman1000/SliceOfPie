using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SliceOfPie
{
    // The Document class is a generalization of the IFileSystemComponentEnum Interface.
    // This means that it can be represented in an "explorer" when placed in Folders.
    // The class itself contains inforamation relevant to the document, and functions
    // to change data in the object, as well as functions to merge the document newer
    // versions of the same document.
    public class Document : IFileSystemComponent
    {
        private string id;
        public string Id { get { return id; } }

        private string path;
        public string Path { get { return path; } set { path = value; } }

        private DocType fileType;
        public DocType FileType { get { return fileType; } }

        private string text;
        public string Text { get { return text; } set { text = value; } }
    
        private string title;
        public string Title { get { return title; } set { title = value; } }

        private User owner;
        public User Owner { get { return owner; } }

        private List<User> sharedWith;
        public List<User> SharedWith { get { return sharedWith; } }

        private Document.DocumentLog log;
        public Document.DocumentLog Log { get { return log; } }

        
        public Document(string text, string title, User owner)
        {
            fileType = DocType.Document;
            this.text = text;
            this.title = title;
            this.owner = owner;
            Path = "root";
            sharedWith = new List<User>();
            log = new Document.DocumentLog(owner);
            CreateId(owner);
        }

        public Document(string text, string title, User owner, List<User> sharedWith)
        {
            this.text = text;
            this.title = title;
            this.owner = owner;
            Path = "root";
            this.sharedWith = sharedWith;
            log = new Document.DocumentLog(owner);
            CreateId(owner);
        }
        
        private void CreateId(User owner)
        {
            id = 8.ToString();
            /*
            TimeSpan t = DateTime.UtcNow - new DateTime(1991, 12, 2);
            int secondsSinceImportantDay = (int)t.TotalSeconds;
            id = (secondsSinceImportantDay.ToString() + owner.ToString());
        
             */ }

        // This functions takes a newer version of this document, and merges it with this one
        // acording to "Simple Merge Policy" given in slice-of-pie.pdf.
        
        public void MergeWith(Document doc, User user)
        {
            List<string> changes = new List<string>();
            // Create original and latest arrays. ( step 1 )
            string[] original = this.GetTextAsArray();
            string[] latest = doc.GetTextAsArray();
            // Create merged array ( made as a list instead ). ( step 2 )
            List<string> merged = new List<string>();
            // Definer o and n index, which point to lines in the original and latest arrays. ( step 3 ) 
            int o = 0;
            int n = 0;
            bool done = false;

            Console.Out.WriteLine("Started merge.");
            // While loop that continues untill the end of both versions of the document have been reached.
                while (!done)   
            {
                Console.Out.WriteLine("New round in while loop");
                // All remaining lines in latest are new. ( step 4 )
                
                if ((o == original.Length) && (n != latest.Length))    
                {
                    Console.Out.WriteLine("Step 4");
                    for (int N=n; N < latest.Length; N++)
                    {   
                        changes.Add("+L"+N+": "+latest[N]);
                        merged.Add(latest[N]);
                        n++;
                    }
                }
                // All remaining lines in original have been removed. ( step 5 )
                    
                else if ((o != original.Length) && (n == latest.Length))
                {
                    Console.Out.WriteLine("Step 5");
                    for (int O=o; O < original.Length; O++)
                    {   
                        changes.Add("-L"+O+": "+original[O]);     
                    }
                    o = original.Length;
                }
                // No changes have occured in this line ( step 6 )
                else if (String.Compare(original[o], latest[n]) == 0)
                {
                    Console.Out.WriteLine("Step 6");
                    merged.Add(original[o]);
                    n++;
                    o++;
                }
                
                else if (String.Compare(original[o], latest[n]) != 0)
                {
                    List<string> temp = new List<string>();
                    bool found = false;
                    int t = 0;
                    for (int N = n; N < latest.Length; N++)
                    {
                        temp.Add(latest[N]);
                        if (String.Compare(original[o], latest[N]) == 0)
                        {
                            found = true;
                            t = N;
                            break;
                        }
                    }
                        // Line has been removed ( Step 7.b )
                        if (found == false)
                        {
                            Console.Out.WriteLine("Step 7.b");
                            changes.Add("-L"+o+": "+original[o]);
                            o++;
                        }
                        // All lines in between have been added ( step 7.c )
                        else
                        {
                            Console.Out.WriteLine("Step 7.c");
                            int counter = 0;
                            foreach (string s in temp)
                            {
                                if (!(counter == temp.Count-1))
                                changes.Add("+L"+n+": "+latest[n]);
                                merged.Add(s);
                                n++;
                                counter++;
                            }
                            o++;
                            n = t + 1;
                        }    
                }
                if (((o == original.Length) && (n == latest.Length)))
                {
                    done = true;
                }
            }
            Console.Out.WriteLine("Merge complete");
            StringBuilder newTextBuilder = new StringBuilder();
            for (int i = 0; i < merged.Count; i++)
            {
                if (!(i == merged.Count-1))
                newTextBuilder.AppendFormat(merged[i] + "\n");
                else
                newTextBuilder.AppendFormat(merged[i]);
            }
           
            text = newTextBuilder.ToString();

            // A log that will document what changes have been made to the document.
            List<string> changeLog = new List<string>();

            bool sharedWithChanged = false;
            bool titleChanged = false;
            bool pathChanged = false;
            bool textChanged = (changes.Count==0);


            // Has there been changes to Sharedwith?
            foreach (User us in doc.SharedWith)
            {
                if (!(this.SharedWith.Contains(us)))
                {
                    this.ShareWith(us);
                    changeLog.Add("Document shared with: " + user.ToString());
                }
                    
            }
            foreach (User us in this.SharedWith)
            {
                if (!(doc.SharedWith.Contains(us)))
                {
                    this.SharedWith.Remove(us);
                    changeLog.Add("Document no longer shared with: " + user.ToString());
                }
            }
            
            // Has title been changed?
            if (String.Compare(doc.Title, this.Title) != 0)
            {
                changeLog.Add("Title has been changed from '" + this.Title + "' to ' " + doc.Title + "'");
                this.title = doc.Title;
            }

            // Has path been changed?
            if (String.Compare(doc.Path, this.Path) != 0)
            {
                changeLog.Add("Path has been changed from '" + this.Path + "' to ' " + doc.Path + "'");
                this.path = doc.Path;
            }

            // Finally, add changes to the text to the changelog.
            changeLog.Add("Changes to the document:\n");

            foreach (string change in changes)
            {
                changeLog.Add(change);
            }

            string sharedWithString ="";
            string titleString = "";
            string pathString = "";
            string textString = "";

            if (titleChanged)
                titleString = "Title. ";

            if (textChanged)
                textString = "Text. ";

            if (sharedWithChanged)
                sharedWithString = "sharedWith. ";
                      
            if (pathChanged)
                pathString = "Path. ";
            
            
            this.Log.AddEntry(new DocumentLog.Entry(user, "Made changed to the following fields : " +titleString+textString+sharedWithString+pathString,changeLog));
        }

        
        // Puts a User on the documents "sharedWith" list, which allows him to access the document.
        public void ShareWith(User user)
        {
            sharedWith.Add(user);
        }

        // Returns a stringArray of the text in the document, that is split on linebreaks.
        private string[] GetTextAsArray()
        {
            string[] temp;

            temp = Regex.Split(text, "\n"); 

            return temp;
        }
        
        // Every Document has its own DocumentLog, which holds logs all information of when 
        // there has been changes to the document.
        public class DocumentLog
        {
            List<Entry> entries;

            public DocumentLog(User user)
            {
                entries = new List<Entry>();
                entries.Add(new Entry(user,"Created the document",null));
            }

            public DocumentLog(List<Entry> list)
            {
                entries = list;
            }
            // Adds an entry to the Log.
            public void AddEntry(Entry entry)
            {
                entries.Add(entry);
            }
            // Gets the newest entry in the log.
            public Entry GetNewestEntry()
            {
                return entries.ElementAt(entries.Count - 1);
            }

            public class Entry
            {
                private User user;
                public User User { get { return user; } }
                
                private DateTime time;
                public DateTime Time { get { return time; }}

                private string description;
                public string Description { get { return description; }}

                private List<string> changeLog;
                public List<string> ChangeLog { get { return changeLog; } }

                public Entry(User u, string desc, List<string> log)
                {
                    time = DateTime.Now;
                    user = u;
                    description = desc;
                }

                public override string ToString()
                {
                    return (user + " " + description + " on the " + time);
                }
            }
        }


        // non-functional interface member
        public void AddChild(IFileSystemComponent child) { }
        public void RemoveChild(IFileSystemComponent child) { }
        public List<IFileSystemComponent> GetChildren() { return null; }

    }
}
