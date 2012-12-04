using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    class DocumentLog
    {
        List<Entry> entries;

        public void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }
    }
}
