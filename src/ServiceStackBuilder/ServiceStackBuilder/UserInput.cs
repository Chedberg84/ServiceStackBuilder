using Onion.SolutionParser.Parser;
using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;

namespace ServiceStackBuilder
{
    public static class UserInput
    {
        public static string sln = "";
        public static string obj = "";

        //Testing values
        //public static string sln = @"C:\GitHub\CostsApi\src\CostsApi.sln";
        //public static string obj = "BadAss";

        public static string Root
        {
            get
            {
                var slnInfo = new FileInfo(sln);
                return slnInfo.DirectoryName;
            }
        }

        public static string TestDir
        {
            get
            {
                var slnInfo = new FileInfo(sln);
                return Path.Combine(slnInfo.Directory.Parent.FullName, "tests");
            }
        }

        public static string SolutionName
        {
            get
            {
                var slnInfo = new FileInfo(sln);
                var name = slnInfo.Name.Substring(0, slnInfo.Name.Length - 4);
                return name;
            }
        }

        public static bool Validate()
        {
            if (string.IsNullOrWhiteSpace(sln) || sln.Length <= 4)
            {
                Console.WriteLine("Sln file was not specified.");
                return false;
            }

            FileInfo slnFile = new FileInfo(sln);
            if(!slnFile.Exists)
            {
                Console.WriteLine("Sln file could not be found.");
                return false;
            }

            if(string.IsNullOrWhiteSpace(obj))
            {
                Console.WriteLine("New object name was not specified.");
                return false;
            }

            var solution = SolutionParser.Parse(UserInput.sln);

            var aatProject = GetProject(solution, BuilderConstants.AATs);
            if(aatProject == null)
            {
                Console.WriteLine("Could not find a project for AATs.");
            }

            var unitTestProject = GetProject(solution, BuilderConstants.UnitTests);
            if (unitTestProject == null)
            {
                Console.WriteLine("Could not find a project for Unit Tests.");
            }

            var modelsProject = GetProject(solution, BuilderConstants.Models);
            if (modelsProject == null)
            {
                Console.WriteLine("Could not find a project for Models.");
            }

            var serviceProject = GetProject(solution, BuilderConstants.Service);
            if (serviceProject == null)
            {
                Console.WriteLine("Could not find a project for Services.");
            }

            var managerProject = GetProject(solution, BuilderConstants.Managers);
            if (managerProject == null)
            {
                Console.WriteLine("Could not find a project for Managers.");
            }

            var repositoryProject = GetProject(solution, BuilderConstants.Repositories);
            if (repositoryProject == null)
            {
                Console.WriteLine("Could not find a project for Repositories.");
            }

            var interfaceProject = GetProject(solution, BuilderConstants.Interfaces);
            if (interfaceProject == null)
            {
                Console.WriteLine("Could not find a project for Interfaces.");
            }


            return true;
        }

        private static Project GetProject(ISolution solution, string name)
        {
            return (from p in solution.Projects
                    where p.Name.ToLower().Contains(name)
                    select p).FirstOrDefault();
        }
    }
}
