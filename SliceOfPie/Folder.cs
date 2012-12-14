﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace SliceOfPie
{
    [DataContract]
    public class Folder : IFileSystemComponent
    {
        [DataMember]
        private string title;
        public string Title{ get { return title; } set { title = value;} }
        [DataMember]
        protected DocType fileType;
        public DocType FileType { get { return fileType; } }
        [DataMember]
        private List<IFileSystemComponent> children;
        public List<IFileSystemComponent> Children { get { return children; } } 

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


        public override bool Equals(object obj)
        {
            if (!(obj is Folder))
                return false;
            Folder f = (Folder)obj;
            if (!(this.FileType == f.FileType && this.Title == f.Title))
            {
               return false;

                
            } 
            for (int i = 0; i < children.Count; i++)
            {
                if (!(children[i].Equals(f.Children[i])))
                {
                    return false;
                }

            }
            return true; 

        }

              
    }
}
