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
        InstanceContextMode=InstanceContextMode.PerSession,
        ConcurrencyMode=ConcurrencyMode.Single)]
    public class SliceOfPieService : ISliceOfPieService
    {
        private Project currentProj;
        private User currentUser;

        /**
         * This is called when an user from the offline client
         * wants to start syncronizing a project with the server.
         * We also make sure the users copy of the project info is up to date
         */
        public Project StartSync(User user, string projectId)
        {
            Console.WriteLine("New user connected to sync offline content");
            currentUser = user;
            currentProj = Storage.GetHierachy(projectId);
            if (currentProj != null)
            {
                if (currentProj.Owner.Equals(user) || currentProj.SharedWith.Contains(user))
                {
                    Console.WriteLine("{0} is now syncing with project: {1},",user,projectId);
                    return currentProj;
                }
            }

            Console.WriteLine("Access Denied: {0} wanted to sync with: {1},"
                +"\n\tMaking client delete local copy of project", user, projectId);
            return null;            
        }

        public void SendDocument(Document doc) 
        {
            // SESSION TEST
            Console.WriteLine("{0} wants to sync document \t{1} \tfrom project \t{2}", currentUser, doc.Title, currentProj);

        }

        public Document GetUpdatedDocument() { return null; }

        public void StopSync() { }




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

        public void SaveProjectOnServer(Project p)
        {
            Storage.SaveProjectToFile(p);
        }

        public void SaveDocumentOnServer(Project p, Document d)
        {
            Storage.WriteToFile(p, d);
        }

        public void DeleteProject(string projectId)
        {
            Storage.DeleteProject(projectId);
        }
    }
}
