using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SliceOfPie;
using SliceOfPieClient.Service;

namespace SliceOfPieClient
{
    /**
     * This controller will handle everything related to server-connection
     * and synchronization with remote content.
     */
    public class ServerController
    {
        /**
         * Shares the changes the user have made in a project
         * to the server, and the server shares changes from other
         * users back. This synchronization is done through a series
         * of method calls to the server.
         */
        public static void SyncWithServer(Project project, User user)
        {
            // Preparing transfer data
            // finding all modified documents to send
            List<DocumentStruct> docs = new List<DocumentStruct>();
            Folder.GetAllStructs(project, docs); // fill up the list

            List<DocumentStruct> docsToSend = new List<DocumentStruct>();
            foreach (DocumentStruct d in docs)
            {
                if (d.Modified)
                    docsToSend.Add(d);
            }


            // Call the server
            using (SliceOfPieServiceClient serviceClient = new SliceOfPieServiceClient())
            {
                // Tell the server who we are and what project we want to sync
                Project newProj = serviceClient.StartSync(user, project.Id);

                // If null is returned, means that the project was deleted
                // or you've lost access to this project, so we delete it
                if (newProj == null)
                    Controller.DeleteProject(project.Id);
                else
                {
                    // Send all modified documents one at a time
                    foreach (DocumentStruct d in docsToSend)
                    {
                        Document document = Controller.OpenDocument(project.Id, d.Id);
                        serviceClient.SendDocument(document);
                        if (document.Deleted)
                            Controller.DeleteDocument(project.Id, document.Id);
                    }

                    // Keep getting new documents from server until there are none remaining
                    while (true)
                    {
                        // Grab a new document from the server
                        Document newDoc = serviceClient.GetUpdatedDocument();

                        // if null is returned, means that there are no new docs left
                        if (newDoc == null)
                            break;

                        // Delete the old copy and write the new one instead
                        Storage.DeleteDocument(project.Id, newDoc.Id);
                        Storage.WriteToFile(newProj, newDoc, true);
                    }

                    // We're done syncing, close the session with the server
                    serviceClient.StopSync();
                }
            }



        }

        /**
         * Asks the server for a list of all the projects for this user
         */
        public static List<Project> GetAllProjectsAvailable(User user)
        {
            List<Project> result;
            using (SliceOfPieServiceClient serviceClient = new SliceOfPieServiceClient())
            {
                result = serviceClient.GetAllProjectsOnServer(user);
            }

            return result;
        }


    }
}
