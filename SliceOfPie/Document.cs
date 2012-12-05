using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SliceOfPie
{
    class Document : IFileSystemComponent
    {
        private string text;
        private string title;
        private User owner;
        private List<User> sharedWith;
        private DocumentLog log;

        public Document(string text, string title, User owner)
        {
            this.text = text;
            this.title = title;
            this.owner = owner;
            sharedWith = new List<User>();
            log = new DocumentLog();
        }

        public string GetText()
        {
            return text;
        }

        public string GetTitle()
        {
            return title;
        }
        
        public void MergeWith(Document doc)
        {
                
        }

        public void ChangeName(String newName)
        {
            title = newName;
        }

        public void ShareWith(User user)
        {
            sharedWith.Add(user);
        }

        public void Edit(string text)
        {

        }

        private string[] CreateTextArray()
        {
            string[] temp;

            temp = Regex.Split(text, "\n"); 

            return temp;
        }
    }
}
