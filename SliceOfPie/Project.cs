using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SliceOfPie
{
    public class Project : Folder
    {
        private User owner;
        public User Owner { get { return owner; } }

        private string id;
        public string Id { get { return id; } }


        private List<User> sharedWith;
        public List<User> SharedWith { get { return sharedWith; } }

        public Project(string title, User owner, List<User> sharedWith) : base(title)
        {
            this.owner = owner;
            this.sharedWith = sharedWith;
            base.fileType = DocType.Project;
            this.sharedWith = sharedWith;
            CreateId();
        }

        private void CreateId()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1991, 12, 2);
            int secondsSinceImportantDay = (int)t.TotalSeconds;
            id = ("p"+secondsSinceImportantDay.ToString() + owner.ToString());
        }
 
        public void shareWith(User user)
        {
            this.sharedWith.Add(user);
        }

        public void unshareWith(User user)
        {
            this.sharedWith.Remove(user);
        }
    }
}
