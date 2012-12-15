using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SliceOfPie;

namespace SliceOfPieServiceLibrary
{
    /**
     * This is where the server behavior goes.
     */
    public class SliceOfPieService : ISliceOfPieService
    {
        /**
         * This method takes the changed documents from a user and 
         * merges these new changes in, and then returns all the new
         * and updated documents back to the user.
         */
        public List<Document> SyncAll(List<SliceOfPie.Document> docs)
        {
            List<Document> result = new List<Document>();
            result.Add(new Document("Little Red Riding Nigga", "Niggerhood", new User("Mister nigger")));
            return result;
        }

        public void DeleteDocument(string projectId, string documentId)
        {
            Storage.ServerDeleteFile(projectId, documentId);
        }

        public List<Project> GetAllProjectsOnServer()
        {
            return Storage.ServerGetAllProjects();
        }

        public Project GetHierachy(string projectId)
        {
            return Storage.ServerGetHierachy(projectId);
        }

        public Document OpenDocumentOnServer(string projectId, string documentId)
        {
            return Storage.ServerReadFromFile(projectId, documentId);
        }

        public void SaveProjectOnServer(SliceOfPie.Project p)
        {
            Storage.ServerSaveProjectToFile(p);
        }

        public void SaveDocumentOnServer(SliceOfPie.Project p, SliceOfPie.Document d)
        {
            

            Storage.ServerWriteToFile(p, d);
        }
    }
}
