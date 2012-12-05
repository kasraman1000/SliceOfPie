using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    class Entry
    {
        private User user;
        private DateTime time;
        private string description;
        private Document earlierVersion;

        public Entry(User u, string desc, Document doc)
        {
            time = DateTime.Now;
            user = u;
            description = desc;
            earlierVersion = doc;
        }

        public DateTime GetTime()
        {
            return time;
        }


        public override string ToString()
        {
            return (user + " " + description + " on the " + time);
        }
    }
}
