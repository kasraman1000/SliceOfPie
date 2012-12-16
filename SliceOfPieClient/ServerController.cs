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
        public static void SyncWithServer(Project project, User user)
        {
            /**
             * Still in development stage
             */


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
                serviceClient.StartSync(user, project.Id);

                foreach (DocumentStruct d in docsToSend)
                {
                    Document document = Controller.OpenDocument(project.Id, d.Id);
                    serviceClient.SendDocument(document);
                }

            }




        }


    }
}
