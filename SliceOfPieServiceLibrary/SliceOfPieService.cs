using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SliceOfPie;

namespace SliceOfPieServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class SliceOfPieService : ISliceOfPieService
    {
        public List<Document> SyncAll(List<Document> docs)
        {
            List<Document> result = new List<Document>();
            result.Add(new Document("Little Red Riding Nigga", "Niggerhood", new User("Mister nigger")));

            return result;
        }
    }
}
