using System;
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
    class Document : IFileSystemComponent
    {
        private string text;
        private string title;
        private User owner;
        private List<User> sharedWith;
        private Document.DocumentLog log;
        

        public Document(string text, string title, User owner)
        {
            this.text = text;
            this.title = title;
            this.owner = owner;
            sharedWith = new List<User>();
            log = new Document.DocumentLog(owner, this);
        }

        public string GetTitle()
        {
            return title;
        }

        public User GetOwner()
        {
            return owner;
        }

        public List<User> GetSharedWith()
        {
            return sharedWith;
        }

        
        // This functions takes a newer version of this document, and merges it this one
        // acording to "Simple Merge Policy" given in slice-of-pie.pdf.
        public void MergeWith(Document doc)
        {
            // Create original and latest arrays. ( step 1 )
            string[] original = this.CreateTextArray();
            Console.Out.WriteLine(original[2]);
            string[] latest = doc.CreateTextArray();
            Console.Out.WriteLine(latest[2]);
            Console.ReadKey();
            // Create merged array ( made as a list instead ). ( step 2 )
            List<string> merged = new List<string>();
            // Definer o and n index, which point to lines in the original and latest arrays. ( step 3 ) 
            int o = 0;
            int n = 0;
            bool done = false;

            Console.Out.WriteLine("Started merge.");
            // While loop that continues untill the end of both versions of the document has been reached.
                while (!done)   
            {
                Console.Out.WriteLine("New round in while loop");
                // All remaining lines in latest are new. ( step 4 )
                
                if ((o == original.Length) && (n != latest.Length))    
                {
                    Console.Out.WriteLine("Step 4");
                    for (int N=n; N < latest.Length; N++)
                    {
                        merged.Add(latest[N]);
                        n++;
                    }
                }
                // All remaining lines in original have been removed. ( step 5 )
                    
                else if ((o != original.Length) && (n == latest.Length))
                {
                    Console.Out.WriteLine("Step 5");
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
                            o++;
                        }
                        // All lines in between have been added ( step 7.c )
                        else
                        {
                            Console.Out.WriteLine("Step 7.c");
                            foreach (string s in temp)
                            {
                                merged.Add(s);
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
            foreach (string s in merged)
            {
                newTextBuilder.AppendFormat(s + "\n");
            }
            text = newTextBuilder.ToString();
            
        }
        // CHange the name of the document.
        public void ChangeName(String newName)
        {
            title = newName;
        }
        // Puts a User on the documents "sharedWith" list, which allows him to access the document.
        public void ShareWith(User user)
        {
            sharedWith.Add(user);
        }
        // Edits the text of the document.
        public void EditText(string text)
        {
            //Some security shoud possibly be added to this function

            this.text = text;
        }

        // Creates a stringArray of the text in the document, that is split on linebreaks.
        public string[] CreateTextArray()
        {
            string[] temp;

            temp = Regex.Split(text, "\n"); 

            return temp;
        }
        // Returns the text in the document.
        public string GetText()
        {
            return text;
        }
        // Returns the document's log.
        public DocumentLog GetLog()
        {
            return log;
        }
        // Every Document has its own DocumentLog, which holds logs all information of when 
        // there has been changes to the document.
        public class DocumentLog
        {
            List<Entry> entries;

            public DocumentLog(User user, Document doc)
            {
                entries = new List<Entry>();
                entries.Add(new Entry(user,"Created the document",doc));
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
                private Document earlierVersion;

                public Entry(User u, string desc, Document doc)
                {
                    time = DateTime.Now;
                    user = u;
                    description = desc;
                    earlierVersion = doc;
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
    }
}
