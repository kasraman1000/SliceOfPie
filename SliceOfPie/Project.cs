using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    class Project : Folder
    {
        private string title;
        public string Title { get { return title; } set { title = value; } }

        private DocType fileType;
        public DocType FileType { get { return fileType; } }

        private User owner;
        public User Owner { get { return owner; } }

        private List<User> sharedWith;
        public List<User> SharedWith { get { return sharedWith; } }

        private List<IFileSystemComponent> children;
        public List<IFileSystemComponent> Children { get { return children; } } 

        public Project(string title, User owner, List<User> sharedWith) : base(title)
        {
            this.owner = owner;
            this.sharedWith = sharedWith;
            fileType = DocType.Project;
        }
         
        public void AddChild(IFileSystemComponent doc)
        {
            children.Add(doc);
        }

        public void RemoveChild(IFileSystemComponent child)
        {
            children.Remove(child);
        }

        public void shareWith(User user)
        {
            this.sharedWith.Add(user);
        }

        public void unshareWith(User user)
        {
            this.sharedWith.Remove(user);
        }
    }
}
