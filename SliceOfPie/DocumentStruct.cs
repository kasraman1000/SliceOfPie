using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SliceOfPie
{
    /**
     * DocumentStruct, a contentless representation of
     * the documents on the system. Mostly used for 
     * building hiearchys such as the treeView in the
     * main window.
     */
    [DataContract]
    [KnownType(typeof(DocumentStruct))]
    public struct DocumentStruct : IFileSystemComponent
    {
        [DataMember]
        private string id;
        public string Id { get { return id; } }

        [DataMember]
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        [DataMember]
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [DataMember]
        private DocType fileType;
        public DocType FileType
        {
            get { return fileType; }
        }

        private bool modified;
        public bool Modified { get { return modified; } }

        private bool deleted;
        public bool Deleted { get { return deleted; } }

        public DocumentStruct(string title, User user, string ID, string path, bool modified, bool deleted)
        {
            id = ID;
            fileType = DocType.Document;
            this.title = title;
            this.path = path;
            this.modified = modified;
            this.deleted = deleted;
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
