using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    struct DocumentStruct : IFileSystemComponent
    {
        private string id;
        public string Id { get { return id; } }

        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private DocType fileType;
        public DocType FileType
        {
            get { return fileType; }
        }

        DocumentStruct(string title, User user, string ID)
        {
            id = ID;
            fileType = DocType.Document;
            this.title = title;
            path = ""; // It won't compile without this :<
        }
    }
}
