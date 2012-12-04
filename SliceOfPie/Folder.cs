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
            return null;
        }

        public void AddChild(Document doc)
        {

        }

        public void RemoveChild(IFileSystemComponent child)
        {

        }

        public List<IFileSystemComponent> GetChildren()
        {
            return children;
        }

    }
}
