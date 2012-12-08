using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    struct DocumentStruct : IFileSystemComponent
    {
        private string id;
        public string Id { get { return id; } }

        private string title;
        public string Title
        {
            get;
            set;
        }

        public DocType FileType
        {
            get;
        }

        DocumentStruct(string title, User user)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1991, 12, 2);
            int secondsSinceImportantDay = (int)t.TotalSeconds;
            id = (secondsSinceImportantDay.ToString() + user.ToString());
            this.title = title;
        }
    }
}
