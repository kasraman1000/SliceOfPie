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
    [ServiceBehavior(
        InstanceContextMode=InstanceContextMode.Single,
        ConcurrencyMode=ConcurrencyMode.Single)]
    public class SliceOfPieService : ISliceOfPieService
    {
        /**
         * This method takes the changed documents from a user and 
         * merges these new changes in, and then returns all the new
         * and updated documents back to the user.
         */
        public List<Document> SyncAll(List<SliceOfPie.Document> docs)
        {
            return null;
        }

        public void DeleteDocument(string projectId, string documentId)
        {
            Storage.DeleteDocument(projectId, documentId);
        }

        public List<Project> GetAllProjectsOnServer()
        {
            return Storage.GetAllProjects();
        }

        public Document OpenDocumentOnServer(string projectId, string documentId)
        {
            return Storage.ReadFromFile(projectId, documentId);
        }

        public void SaveProjectOnServer(SliceOfPie.Project p)
        {
            Storage.SaveProjectToFile(p);
        }

        public void SaveDocumentOnServer(SliceOfPie.Project p, SliceOfPie.Document d)
        {
            

            Storage.WriteToFile(p, d);
        }

        public void DeleteProject(string projectId)
        {
            Storage.DeleteProject(projectId);
        }
    }
}
