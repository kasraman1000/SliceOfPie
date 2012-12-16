using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

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
