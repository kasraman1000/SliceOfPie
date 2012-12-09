using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    public interface IFileSystemComponent
    {
        string Title
        {
            get;
            set;
        }

        DocType FileType
        {
            get;
        }
    }
}
