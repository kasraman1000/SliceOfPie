using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    public interface IFileSystemComponent
    {
        String GetTitle();
        void AddChild(IFileSystemComponent child);
        void RemoveChild(IFileSystemComponent child);
        List<IFileSystemComponent> GetChildren();
    }
}
