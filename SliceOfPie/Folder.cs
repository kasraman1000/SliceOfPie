using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    class Folder : IFileSystemComponent
    {
        string title;
        List<IFileSystemComponent> children;
        bool isRoot;

        public string GetTitle()
        {
            return title;
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
