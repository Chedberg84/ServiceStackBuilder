using Onion.SolutionParser.Parser;
using Onion.SolutionParser.Parser.Model;
using ServiceStackBuilder.Builders;
using System;
using System.Collections.Generic;

namespace ServiceStackBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            CollectUserInput();
            
            bool valid = UserInput.Validate();
            if(valid)
            {
                Console.WriteLine("Would you like me to get started?");
                SayPlease();

                Console.WriteLine("Processing...");

                Process();

                Console.WriteLine("Done doing your work for you. You're welcome.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("User input was not valid. Please try again.");
                CollectUserInput();
            }
        }

        static void CollectUserInput()
        {
            Console.WriteLine("Welcome to the ServiceStack Builder console application.");
            Console.WriteLine("Location of sln file?");
            UserInput.sln = Console.ReadLine();

            Console.WriteLine("New request object name.");
            UserInput.obj = Console.ReadLine();
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

            builders.Add(new MessageBuilder(solution));
            builders.Add(new ModelBuilder(solution));
            builders.Add(new InterfaceBuilder(solution));
            builders.Add(new ManagerBuilder(solution));
            builders.Add(new RepositoryBuilder(solution));
            builders.Add(new ServiceBuilder(solution));
            builders.Add(new AppHostBuilder(solution));
            builders.Add(new UnitTestBuilder(solution));
            builders.Add(new AcceptanceTestBuilder(solution));
            
            return builders;
        }
    }
}
