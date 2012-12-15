using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SliceOfPie;
using SliceOfPieClient.Service;


namespace EmptyProject
{
    class KekClassTest
    {
        public static void Main(String[] args)
        {
            SliceOfPieServiceClient serviceClient = new SliceOfPieServiceClient();

            var response = serviceClient.SyncAll(new List<SliceOfPie.Document>());

            Console.WriteLine(response);
            Console.ReadKey();
        }
    }
}
