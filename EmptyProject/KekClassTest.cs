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
                List<SliceOfPie.Project> list = serviceClient.GetAllProjectsOnServer();

                Console.ReadKey();
            }

           
        }
    }
}
