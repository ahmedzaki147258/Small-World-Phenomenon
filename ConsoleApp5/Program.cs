using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SmallWorld;

namespace project_algo
{
    class Program
    {
        static void Main(string[] args)
        {

            int choice = 0;
            Console.WriteLine("Enter: \n1-> For the Queries.\n2-> For Optimizetion.\n3-> for Bonuse.");
            choice = int.Parse(Console.ReadLine());

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            ReadData RD = new ReadData();
            RD.ReadSample(choice);

            if (choice == 3) return;

            RD.ReadQueries(choice);
            stopwatch.Stop();


            Console.WriteLine("the program finished");
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}