using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    public class Folder : IFileSystemComponent
    {
        private string title;
        public string Title{get{ return title; } set{ title = value;} }
        List<IFileSystemComponent> children;
        bool isRoot;

        public Folder(string title)
        {
            this.title = title;
            children = new List<IFileSystemComponent>();
        }

        public void AddChild(IFileSystemComponent doc)
        {
            children.Add(doc);
        }

        public void RemoveChild(IFileSystemComponent child)
        {
            children.Remove(child);
        }

        public List<IFileSystemComponent> GetChildren()
        {
            return children;
        }

    }
}
