using Onion.SolutionParser.Parser.Model;
using System;
using System.Linq;
using System.Reflection;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Web;
using System.IO;

namespace ServiceStackBuilder.Builders
{
    public class AppHostBuilder : BuilderBase
    {
        private ISolution Solution { get; set; }

        public AppHostBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building AppHost");
            
            string workingDir = Path.Combine(UserInput.Root, UserInput.SolutionName);
            string appHostFile = Path.Combine(workingDir, "AppHost.cs");

            string[] lines = File.ReadAllLines(appHostFile);

            int index = -1;
            for(int i=0; i < lines.Length; i++)
            {
                if(lines[i].ToLower().Contains("container.register"))
                {
                    index = i;
                }
            }

            //add to the index line
            if(index > -1)
            {
                string managerContainer = $"            container.RegisterAs<{UserInput.obj}Repository, I{UserInput.obj}Repository>();";
                string repositoryContainer = $"            container.RegisterAs<{UserInput.obj}Manager, I{UserInput.obj}Manager>();";
                lines[index] = lines[index] + Environment.NewLine + managerContainer + 
                    Environment.NewLine + repositoryContainer + Environment.NewLine;
            }
            else
            {
                //look through other files for container registrations

                //if nothing found, look for "public override void Configure("
            }

            //write the file changes to disk.
            File.WriteAllLines(appHostFile, lines);

            //use regex to find the right place to inject container registration

            
        }
    }
}
