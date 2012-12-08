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

        private IFileSystemComponentEnum.docType fileType;
        public IFileSystemComponentEnum.docType FileType { get { return fileType; } }

        List<IFileSystemComponent> children;
        bool isRoot;

        public Folder(string title)
        {
            fileType = IFileSystemComponentEnum.docType.Folder;
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
