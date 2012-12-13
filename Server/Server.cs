using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SliceOfPieServiceLibrary;

namespace Server
{
    /**
     * This application starts the server.
     */
    class Server
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(SliceOfPieService)))
            {
                // In here, the service is now open
                host.Open();

                Console.WriteLine("SliceOfPie Service now running!");
                Console.ReadKey();
            }
        }
    }
}
