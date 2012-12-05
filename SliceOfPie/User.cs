using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    class User
    {
        private string name;

        public User(string n)
        {
            name = n;
        }

        public override string ToString()
        {
            return name;
        }
    }  
}
