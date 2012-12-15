using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;

namespace SliceOfPie
{
    [DataContract]
    public class Picture  
    {
        [DataMember]
        private string id;
        public string Id { get { return id; } set { id = value; } }

        [DataMember]
        private Bitmap image;
        public Bitmap Image { get { return image; } set { image = value; } }

        public Picture(Bitmap image)
        {
            this.image = image;
            CreateId();
        }

        public Picture(Bitmap image, string id)
        {
            this.image = image;
            this.id = id;
        }

        private void CreateId()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1991, 12, 2);
            int secondsSinceImportantDay = (int)t.TotalSeconds;
            id = ("p"+secondsSinceImportantDay.ToString());
        }

        public override string ToString()
        {
            return id;
        }

        public override bool Equals(object obj)
        {
            Picture pic;
            try
            {
                pic = (Picture)obj;
            }
            catch (Exception)
            {
                return false;
            }


            if (String.Compare(this.id, pic.Id) == 0)
                return true;
            else
                return false;
        }
    }
}
