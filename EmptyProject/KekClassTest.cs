using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SliceOfPie;
using SliceOfPieClient.Service;
using System.Threading;


namespace EmptyProject
{
    class KekClassTest
    {
        public static void Main(String[] args)
        {
            Thread.Sleep(1000);
            using (SliceOfPieServiceClient serviceClient = new SliceOfPieServiceClient())
            {
                Project p = new Project("SERVERTEST", new User("servertestuserman"), new List<User>());

                //var derp = serviceClient.SyncAll(new List<Document>());

                //List<Project> list = Storage.ServerGetAllProjects();
                //List<SliceOfPie.Project> list = serviceClient.GetAllProjectsOnServer();
                //var herp = serviceClient.GetHierachy("p663555625Crelde");

                Project bool1 = serviceClient.StartSync(new User("Crelde"), "p663555625Crelde");
                //Project fool2 = serviceClient.StartSync(new User("Creldz"), "p663555625Crelde");
                //Project fool3 = serviceClient.StartSync(new User("Crelde"), "p3555625Crelde");
                //Project bool4 = serviceClient.StartSync(new User("Motor-Bjarne"), "p663555625Crelde");


                serviceClient.SendDocument(new Document("", "Document first", new User("Crelde")));
                serviceClient.SendDocument(new Document("", "Document 2", new User("Crelde")));
                serviceClient.SendDocument(new Document("", "Document third", new User("Crelde")));
                serviceClient.SendDocument(new Document("", "4th document", new User("Crelde")));
                serviceClient.SendDocument(new Document("", "half tenth doc", new User("Crelde")));
                serviceClient.SendDocument(new Document("", "sexy doc", new User("Crelde")));
                serviceClient.SendDocument(new Document("", "doc triple 7", new User("Crelde")));

                Console.ReadKey();
            }

           
        }
    }
}
