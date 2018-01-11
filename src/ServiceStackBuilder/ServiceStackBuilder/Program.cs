using System;
using Onion.SolutionParser.Parser;
using System.Linq;
using Onion.SolutionParser.Parser.Model;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using ServiceStackBuilder.Workers;
using System.Collections.Generic;

namespace ServiceStackBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Welcome to the ServiceStack Builder console application.");
            //Console.WriteLine("Location of sln file?");
            //sln = Console.ReadLine();

            //Console.WriteLine("New request object name.");
            //obj = Console.ReadLine();

            //VERIFY THE USER INPUT HERE!!
            //var slnInfo = new FileInfo(UserInput.sln);

            Console.WriteLine("Would you like me to get started?");
            SayPlease();

            Console.WriteLine("Processing...");

            Process();

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

        static void Process()
        {
            var solution = SolutionParser.Parse(UserInput.sln);
            var builders = GetBuilders(solution);
            builders.ForEach(x => x.Go());
        }

        static List<IBuilder> GetBuilders(ISolution solution)
        {
            List<IBuilder> builders = new List<IBuilder>();

            //models
            builders.Add(new ModelBuilder(solution));
            builders.Add(new InterfaceBuilder(solution));
            builders.Add(new ManagerBuilder(solution));
            builders.Add(new RepositoryBuilder(solution));

            return builders;
            
            //Find the service deffinition
            //Create a new service class with CRUD methods for this request object
            //update service deffinition project file
            
            //Find the apphost file (or container manager file)
            //update this file with new interface mappings

            //DONE
        }
    }
}
