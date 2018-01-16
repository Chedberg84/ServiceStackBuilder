using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ServiceStackBuilder.Builders
{
    public class InterfaceBuilder : BuilderBase
    {
        private ISolution Solution { get; set; }

        public InterfaceBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building Interfaces");

            var project = GetProject(Solution, BuilderConstants.Interfaces);

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name, UserInput.obj);
            Directory.CreateDirectory(workingDir);

            BuildInterface(workingDir, project);
        }

        private void BuildInterface(string workingDir, Project project)
        {
            string managerTemplatePath = "Templates\\Manager\\ManagerInterfaceTemplate.txt";
            string repositoryTemplatePath = "Templates\\Repository\\RepositoryInterfaceTemplate.txt";

            string managerFileName = GenericBuild(workingDir, managerTemplatePath, "I", "Manager");
            string repositoryFileName = GenericBuild(workingDir, repositoryTemplatePath, "I", "Repository");

            string[] fileNames = new string[] { managerFileName, repositoryFileName };
            GenericProjectUpdate(project, fileNames, UserInput.obj);
        }
    }
}
