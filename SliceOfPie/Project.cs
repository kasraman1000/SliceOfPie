using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace SliceOfPie
{
    [DataContract]
    [KnownType(typeof(Project))]
    [KnownType(typeof(Folder))]
    [KnownType(typeof(DocumentStruct))]
    public class Project : Folder
    {
        [DataMember]
        private User owner;
        public User Owner { get { return owner; } }

        [DataMember]
        private string id;
        public string Id { get { return id; } }

        [DataMember]
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

        public Project(string title, User owner, List<User> sharedWith, string id) : base(title)
        {
            this.owner = owner;
            this.sharedWith = sharedWith;
            base.fileType = DocType.Project;
            this.sharedWith = sharedWith;
            this.id = id;
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

        public override string ToString()
        {
            return Title;
        }

		public override bool Equals(object obj)
		{
			if (!(obj is Project))
				return false;
			Project p = (Project)obj;
			if (!(this.FileType == p.FileType 
                && this.id == p.Id
                && this.Title == p.Title
                && String.Compare(this.Title,p.Title)==0))
				return false;
            
            for (int i = 0; i < sharedWith.Count; i++)
            {
                if (!(sharedWith[i].Equals(p.sharedWith[i])))
                {
                    return false;
                }
            }

            return true;

        }
    }
}
