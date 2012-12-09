using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    public class Folder : IFileSystemComponent
    {
        private string title;
        public string Title{ get { return title; } set { title = value;} }

        private DocType fileType;
        public DocType FileType { get { return fileType; } }

        List<IFileSystemComponent> children;
        List<IFileSystemComponent> Children { get { return children; } } 

        // Deprecated, remove please:
        public List<IFileSystemComponent> GetChildren() { return null; }

        public Folder(string title)
        {
            fileType = DocType.Folder;
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

              
    }
}
