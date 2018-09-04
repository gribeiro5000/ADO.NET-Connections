using FinalFantasyI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Connections
{
    class Program
    {
        static void Main(string[] args)
        {
            var rep= new Repository();

            var result = rep.GetAll<Job>("Job");

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
