using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;

namespace ServiceStackBuilder.Builders
{
    public class RepositoryBuilder : BuilderBase
    {
        private ISolution Solution { get; set; }

        public RepositoryBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building Repositories");

            var project = GetProject(Solution, BuilderConstants.Repositories);

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name, "CRUD");

            //create request and response files
            Directory.CreateDirectory(workingDir);

            Build(workingDir, project);
        }

        private void Build(string workingDir, Project project)
        {
            string templatePath = "Templates\\Repository\\RepositoryTemplate.txt";

            string fileName = GenericBuild(workingDir, templatePath, "", "Repository");

            string[] fileNames = new string[] { fileName };
            GenericProjectUpdate(project, fileNames, "CRUD");
        }
    }
}
