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
        private List<DocumentStruct> docsToSend;

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

        /**
         * Recieve updated documents from the client and merge these changes in
         */
        public void SendDocument(Document doc) 
        {
            

            if (doc.Deleted)
            {
                Console.WriteLine("{0} deleted document \t{1}", currentUser, doc.Title);
                Controller.DeleteDocument(currentProj.Id, doc.Id);

            }
            else
            {
                Console.WriteLine("{0} changed document \t{1}", currentUser, doc.Title);
                Controller.SaveDocument(currentProj, doc, currentUser);
            }
                
        }

        public Document GetUpdatedDocument() 
        {
            // The first time this is called, fill up the list of docs to send
            if (docsToSend == null)
            {
                Console.WriteLine("{0} is now ready to recieve files", currentUser);
                docsToSend = new List<DocumentStruct>();
                Folder.GetAllStructs(Storage.GetHierachy(currentProj.Id), docsToSend);
            }

            // if it's out of documents to send back, return null
            if (docsToSend.Count == 0)
            {
                Console.WriteLine("No more documents to send to {0}", currentUser);
                return null;
            }

            // And if there's still docs left to be sent, pop one off the list and send it
            Document result = Controller.OpenDocument(currentProj.Id, docsToSend[0].Id);
            docsToSend.RemoveAt(0);
            Console.WriteLine("Sending {0} to {1}...", result.Title, currentUser);
            return result;
        }

        public void StopSync()
        {
            Console.WriteLine("{0} has finished syncing, good bye!", currentUser);
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
