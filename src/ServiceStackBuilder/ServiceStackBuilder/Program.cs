using System;

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
            //figure out where we are in the sln file.

            //Find the modles project
            //create a new folder for this request object
            //Create request
            //Create response
            //udpate the modles project file


            //Find the interfaces project
            //add new Manager interface
            //add new Repository interface
            //update interface project file


            //Find the service deffinition
            //Create a new service class with CRUD methods for this request object
            //update service deffinition project file


            //Find the managers project
            //add a new manager file
            //update the managers project file

            //Find the repository project
            //add a new repository file
            //update repository project

            //Find the apphost file (or container manager file)
            //update this file with new interface mappings
            
            //DONE
        }
    }
}
