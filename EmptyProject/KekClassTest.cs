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
                List<SliceOfPie.Project> list = serviceClient.GetAllProjectsOnServer();
                //var herp = serviceClient.GetHierachy("p663555625Crelde");


                Console.ReadKey();
            }

           
        }
    }
}
