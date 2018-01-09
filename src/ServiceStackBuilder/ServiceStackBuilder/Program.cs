using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the ServiceStack Builder console application.");
            Console.WriteLine("Location of sln file?");
            var sln = Console.ReadLine();

            Console.WriteLine("New request object name.");
            var obj = Console.ReadLine();

            Console.WriteLine("New request object route");
            var route = Console.ReadLine();

            Console.WriteLine("Would you like me to get started?");
            SayPlease();

            Console.WriteLine("Processing...");

            Process(sln, obj, route);

            Console.WriteLine("Done doing your work for you. You're welcome.");
            Console.ReadLine();
        }

        static bool SayPlease()
        {
            var please = Console.ReadLine();

            bool sayPlease = false;
            while(sayPlease == false)
            {
                if(please.ToLower().Equals("no"))
                {
                    Environment.Exit(0);
                }
                else if(please.ToLower().Contains("yes please"))
                {
                    sayPlease = true;
                }
                else
                {
                    Console.WriteLine("Na ah ah, you didn't say the magic word.");
                    please = Console.ReadLine();
                }
            }
            
            return true;
        }

        static void Process(string sln, string obj, string route)
        {

        }
    }
}
