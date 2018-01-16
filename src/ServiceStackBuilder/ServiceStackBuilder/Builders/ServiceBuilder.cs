using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ServiceStackBuilder.Builders
{
    public class ServiceBuilder : BuilderBase
    {
        private ISolution Solution { get; set; }

        public ServiceBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building Service");

            var project = GetProject(Solution, BuilderConstants.Service);

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name);

            Build(workingDir, project);
        }

        private void Build(string workingDir, Project project)
        {
            string templatePath = "Templates\\Service\\ServiceTemplate.txt";

            string fileName = GenericBuild(workingDir, templatePath, "", "Service");

            string[] fileNames = new string[] { fileName };
            GenericProjectUpdate(project, fileNames);
        }
    }
}
