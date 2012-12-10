using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    public struct DocumentStruct : IFileSystemComponent
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

        public DocumentStruct(string title, User user, string ID, string path)
        {
            id = ID;
            fileType = DocType.Document;
            this.title = title;
            this.path = path;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is DocumentStruct))
                return false;
            DocumentStruct d = (DocumentStruct)obj;
            if (this.Id == d.Id && this.Path == d.Path && this.Title == d.Title && this.FileType == d.FileType)
            {
                return true;
            }
            return false;

           
        }
    }
}
