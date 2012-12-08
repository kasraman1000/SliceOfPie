﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SliceOfPie
{
    // The Document class is a generalization of the IFileSystemComponent Interface.
    // This means that it can be represented in an "explorer" when placed in Folders.
    // The class itself contains inforamation relevant to the document, and functions
    // to change data in the object, as well as functions to merge the document newer
    // versions of the same document.
    public class Document : IFileSystemComponent
    {
        private IFileSystemComponentEnum.docType fileType;
        public IFileSystemComponentEnum.docType FileType { get { return fileType; } }

        private string text;
        public string Text { get { return text; } set { text = value; } }
    
        private string title;
        public string Title { get { return title; } set { title = value; } }

        private User owner;
        public User Owner { get { return owner; } set { owner = value; } }

        private List<User> sharedWith;
        public List<User> SharedWith { get { return sharedWith; } set { sharedWith = value; } }

        private Document.DocumentLog log;
        public Document.DocumentLog Log { get { return log; } set { log = value; } }

        
        public Document(string text, string title, User owner)
        {
            fileType = IFileSystemComponentEnum.docType.Document;
            this.text = text;
            this.title = title;
            this.owner = owner;
            sharedWith = new List<User>();
            log = new Document.DocumentLog(owner);
        }

        public Document(string text, string title, User owner, List<User> sharedWith)
        {
            this.text = text;
            this.title = title;
            this.owner = owner;
            this.sharedWith = sharedWith;
            log = new Document.DocumentLog(owner);
        }
         
        // This functions takes a newer version of this document, and merges it with this one
        // acording to "Simple Merge Policy" given in slice-of-pie.pdf.
        public List<string> MergeWith(Document doc)
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
            return changes;
        }
        
        // Puts a User on the documents "sharedWith" list, which allows him to access the document.
        public void ShareWith(User user)
        {
            sharedWith.Add(user);
        }
        // Edits the text of the document.
       
        // Returns a stringArray of the text in the document, that is split on linebreaks.
        public string[] GetTextAsArray()
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
                entries.Add(new Entry(user,"Created the document"));
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
                private DateTime time;
                private string description;
                private List<string> earlierVersion;

                public Entry(User u, string desc)
                {
                    time = DateTime.Now;
                    user = u;
                    description = desc;
                }

                public DateTime GetTime()
                {
                    return time;
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
