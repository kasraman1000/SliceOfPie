using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    class Document : IFileSystemComponent
    {
        string text;
        string title;
        User owner;
        List<User> sharedWith;
        DocumentLog log;

        public string GetTitle()
        {
            return null;
        }
        
        public void MergeWith(Document doc)
        {

        }

        public void ChangeName(String newName)
        {

        }

        public void ShareWith(int userId)
        {

        }

        public void Edit(string text)
        {

        }
    }
}
