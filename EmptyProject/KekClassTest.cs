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

                bool bool1 = serviceClient.StartSync(new User("Crelde"), "p663555625Crelde");
                bool fool2 = serviceClient.StartSync(new User("Creldz"), "p663555625Crelde");
                bool fool3 = serviceClient.StartSync(new User("Crelde"), "p3555625Crelde");
                bool bool4 = serviceClient.StartSync(new User("Motor-Bjarne"), "p663555625Crelde");




                Console.ReadKey();
            }

           
        }
    }
}
