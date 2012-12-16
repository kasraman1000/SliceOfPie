using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Drawing;

namespace SliceOfPie
{
    // The Document class is a generalization of the IFileSystemComponentEnum Interface.
    // This means that it can be represented in an "explorer" when placed in Folders.
    // The class itself contains inforamation relevant to the document, and functions
    // to change data in the object, as well as functions to merge the document newer
    // versions of the same document.
    [DataContract]
    public class Document
    {
        [DataMember]
        private string id;
        public string Id { get { return id; } }

        [DataMember]
        private string path;
        public string Path { get { return path; } set { path = value; } }

        [DataMember]
        private string text;
        public string Text { get { return text; } set { text = value; } }

        [DataMember]
        private User owner;
        public User Owner { get { return owner; } set { owner = value; } }

        [DataMember]
        private string title;
        public string Title { get { return title; } set { title = value; } }

        [DataMember]
        private List<Picture> images;
        public List<Picture> Images { get { return images; } set { images = value; } }

        [DataMember]
        private Document.DocumentLog log;
        public Document.DocumentLog Log { get { return log; } }

        [DataMember]
        private bool deleted = false;
        public bool Deleted { get { return deleted; } set { deleted = value; } }

        [DataMember]
        private bool modified = true;
        public bool Modified { get { return modified; } set { modified = value; } }

        // Default constructor for creating a document object.
        public Document(string text, string title, string path, User owner)
        {
            this.text = text;
            this.title = title;
            this.owner = owner;
            this.path = path;
            this.images = new List<Picture>();
            log = new Document.DocumentLog(owner);
            CreateId();
        }

        // This is primarily used by tests.
        public Document(string text, string title, User owner)
        {
            this.text = text;
            this.title = title;
            this.owner = owner;
            this.images = new List<Picture>();
            log = new Document.DocumentLog(owner);
            CreateId();
        }

        // Same as constructor above, but this one allows to set id as well.
        // This is primarily used by tests.
        public Document(string text, string title, User owner, string id)
        {
            this.text = text;
            this.title = title;
            this.owner = owner;
            this.images = new List<Picture>();
            log = new Document.DocumentLog(owner);
            this.id = id;
        }

        // Private construtor that is only used by CreateDocumentFromFile
        private Document(){}

        // Creates and returns a document in it's complete version from a file on the file system.
        public static Document CreateDocumentFromFile(string id, string text, string title, bool modified, bool deleted, User owner, List<Picture> pictures, string path, Document.DocumentLog log)
        {
            Document doc = new Document();
            doc.id = id;
            doc.text = text;
            doc.title = title;
            doc.owner = owner;
            doc.path = path;
            doc.log = log;
            doc.images = pictures;
            doc.modified = modified;
            doc.deleted = deleted;
            return doc;
        }

        private void CreateId()
        {           
            TimeSpan t = DateTime.UtcNow - new DateTime(1991, 12, 2);
            int secondsSinceImportantDay = (int)t.TotalSeconds;
            id = (secondsSinceImportantDay.ToString() + owner.ToString());
        }

        // This functions takes a newer version of this document, and merges it with this one
        // acording to "Simple Merge Policy" given in slice-of-pie.pdf.
        // It also updated all other fields in the document according to the new version
        // and generates a log based on the changes.
        // MergeWith returns a bool as well, it returns false if the ID of the updated
        // document is not the same as this documents ID, otherwise it returns true
        public bool MergeWith(Document doc, User user)
        {
            if (this.id != doc.id)
                return false;
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

            bool titleChanged = false;
            bool pathChanged = false;
            bool textChanged = (!(changes.Count==0));
            bool picturesAdded = false;
            bool picturesRemoved = false;

            List<Picture> imagesAdded = new List<Picture>();
            List<Picture> imagesRemoved = new List<Picture>();
                                    
            // Has title been changed?
            if (String.Compare(doc.Title, this.Title) != 0)
            {
                changeLog.Add("Title has been changed from '" + this.Title + "' to '" + doc.Title + "'");
                this.title = doc.Title;
                titleChanged = true;
            }

            // Has path been changed?
            if (String.Compare(doc.Path, this.Path) != 0)
            {
                changeLog.Add("Path has been changed from '" + this.Path + "' to '" + doc.Path + "'");
                this.path = doc.Path;
                pathChanged = true;
            }

            // Are there any new pictures?
            foreach (Picture pic in doc.Images)
            {
                if (!(this.Images.Contains(pic)))
                {
                    picturesAdded = true;
                    imagesAdded.Add(pic);
                }
            }
          

            // Are any of the old pictures removed?
            foreach (Picture pic in this.Images)
            {
                if (!(doc.Images.Contains(pic)))
                {
                    picturesRemoved = true;
                    imagesRemoved.Add(pic);
                }
            }
            foreach (Picture pic in imagesRemoved)
            {
                images.Remove(pic);
                pic.Image.Dispose();
            }

            foreach (Picture pic in imagesAdded)
            {
                images.Add(pic);
            }
            // If there were pictures added, add it to the changelog.
            if (picturesAdded)
            {
                StringBuilder imageLineBuilder = new StringBuilder();

                for (int i = 0; i < imagesAdded.Count; i++)
                {
                    if (i == imagesAdded.Count - 1)
                        imageLineBuilder.AppendFormat(imagesAdded[i].Id);
                    else
                        imageLineBuilder.AppendFormat(imagesAdded[i].Id + ",");
                }
                changeLog.Add("Following pictures were added: " + imageLineBuilder.ToString());
            }

            // If there were pictures removed, add it to the changelog.
            if (picturesRemoved)
            {
                StringBuilder imageLineBuilder = new StringBuilder();

                for (int i = 0; i < imagesRemoved.Count; i++)
                {
                    if (i == imagesRemoved.Count - 1)
                        imageLineBuilder.AppendFormat(imagesRemoved[i].Id);
                    else
                        imageLineBuilder.AppendFormat(imagesRemoved[i].Id + ", ");
                }
                changeLog.Add("Following pictures were removed: " + imageLineBuilder.ToString());
            }

            // Finally, add changes to the text to the changelog.
            if (textChanged)
                changeLog.Add("Changes to the documents text:");

            foreach (string change in changes)
            {
                changeLog.Add(change);
            }

            string titleString = "";
            string pathString = "";
            string textString = "";
            string pictureString = "";
            string masterString = "Changed the document's: ";

            if (titleChanged)
                titleString = "Title. ";

            if (textChanged)
                textString = "Text. ";
                      
            if (pathChanged)
                pathString = "Path. ";

            if ((picturesAdded || picturesRemoved)&&textChanged)
                pictureString = " And changed attached pictures";
            else if ((picturesAdded || picturesRemoved) && textChanged == false)
            {
                masterString = "";
                pictureString = "Changed the attached pictures";
            }


            
            this.Log.AddEntry(new DocumentLog.Entry(user, masterString + titleString + textString + pathString + pictureString, changeLog));

            return true;
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
        [DataContract]
        public class DocumentLog
        {
            [DataMember]
            public List<Entry> entries;

            public DocumentLog(User user)
            {
               entries = new List<Entry>();
                List<string> emptyLog = new List<string>();
                entries.Add(new Entry(user,"created the document",emptyLog));
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

            public override string ToString()
            {
                StringBuilder temp = new StringBuilder();

                temp.AppendFormat("Log:\n");
                
                int counter = 0;

                foreach (Entry entry in entries)
                {
                    temp.AppendFormat("Entry" + counter +"\n");
                    counter++;
                    temp.AppendFormat(entry.ToStringWithLog());
                }

                temp.AppendFormat("---------------ENDOFLOG------------------");
                return temp.ToString();
            }

            [DataContract]
            public class Entry
            {
                [DataMember]
                private User user;
                public User User { get { return user; } }

                [DataMember]
                private DateTime time;
                public DateTime Time { get { return time; }}

                [DataMember]
                private string description;
                public string Description { get { return description; }}

                [DataMember]
                private List<string> changeLog;
                public List<string> ChangeLog { get { return changeLog; } }

                // Constructor that creates an Entry with the parameters specified, as well 
                // as defining the DateTime to be the second the entry object was created.
                public Entry(User u, string desc, List<string> log)
                {
                    time = DateTime.Now;
                    user = u;
                    description = desc;
                    changeLog = log;
                }

                // Constructor that allows specifying when the Entry is from.
                public Entry(User u, string desc, List<string> log, DateTime time)
                {
                    user = u;
                    description = desc;
                    changeLog = log;
                    this.time = time;
                }

                public override string ToString()
                {
                    return (user + " " + description + " on the " + time+"\n");
                }

                // The ToStringWithLog is usedwhen writing the Entry to the file system.
                public string ToStringWithLog()
                {
                    StringBuilder temp = new StringBuilder();

                    temp.AppendFormat(ToString());

                    foreach (string entry in changeLog)
                    {
                        if (String.Compare(entry,"")==0)
                            temp.AppendFormat(entry);
                        else
                            temp.AppendFormat(entry+"\n");
                    }
                    return temp.ToString();
                }
            }
        }
    }
}
