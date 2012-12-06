using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    public class Folder : IFileSystemComponent
    {
        string title;
        IFileSystemComponentEnum.docType fileType;
        List<IFileSystemComponent> children;
        bool isRoot;

        public Folder(string title)
        {
            fileType = IFileSystemComponentEnum.docType.Folder;
            this.title = title;
            children = new List<IFileSystemComponent>();
        }


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

        public IFileSystemComponentEnum.docType GetDocType()
        {
            return fileType;
        }
    }
}
