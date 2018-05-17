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
            FileInfo[] files = new DirectoryInfo(workingDir).GetFiles();
            string[] lines = null;
            int index = -1;
            string FileLocation = null;

            foreach(var file in files)
            {
                if(file.Extension.ToLower().Equals(".cs"))
                {
                    lines = File.ReadAllLines(file.FullName);
                    index = GetContainerRegistrationIndex(lines);

                    //if we find a registration point in the code file, exit loop.
                    if(index > -1)
                    {
                        FileLocation = file.FullName;
                        break;
                    }
                }
            }
            
            //add to the index line
            if(index > -1 && lines != null)
            {
                string managerContainer = $"            container.RegisterAs<{UserInput.obj}Repository, I{UserInput.obj}Repository>();";
                string repositoryContainer = $"            container.RegisterAs<{UserInput.obj}Manager, I{UserInput.obj}Manager>();";
                lines[index] = lines[index] + Environment.NewLine + Environment.NewLine + managerContainer + 
                    Environment.NewLine + repositoryContainer;

                //write the file changes to disk.
                File.WriteAllLines(FileLocation, lines);
            }
            else
            {
                Console.WriteLine("WARNING: A container registration method was not found in the main project.");
            }
        }
        
        private int GetContainerRegistrationIndex(string[] lines)
        {
            int index = -1;
            for (int i = 0; i < lines.Length; i++)
            {
                //if (lines[i].ToLower().Contains(BuilderConstants.ContainerRegistration))
                //{
                //    index = i;
                //}
            }

            return index;
        }
    }
}
