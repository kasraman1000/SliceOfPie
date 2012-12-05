using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    class User
    {
        private string name;
        private int id;

        public User(string n, int i)
        {
            name = n;
            id = i;
        }

        public override string ToString()
        {
            return name;
        }
    }  
}
