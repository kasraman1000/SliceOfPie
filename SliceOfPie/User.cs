using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SliceOfPie
{
    [DataContract]
    public class User
    {
        [DataMember]
        private string name;

        public User(string n)
        {
            name = n;
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is User))
                return false;
            User u = (User)obj;
            if (!(this.name == u.name))
            {
               return false;
            } 
            
            return true; 
        }
    }  
}
