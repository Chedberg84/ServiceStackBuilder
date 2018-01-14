using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;

namespace ServiceStackBuilder.Builders
{
    public class AcceptanceTestBuilder : BuilderBase
    {
        private ISolution Solution { get; set; }

        public AcceptanceTestBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building AATs");

            var project = (from p in Solution.Projects where p.Name.ToLower().Contains("acceptancetest") select p).FirstOrDefault();

            //Define the working directory
            string workingDir = Path.Combine(UserInput.TestDir, project.Name);

            Build(workingDir, project);
        }

        private void Build(string workingDir, Project project)
        {
            string templatePath = "Templates\\AAT\\AatTemplate.txt";

            string fileName = GenericBuild(workingDir, templatePath, "", "Tests");

            string[] fileNames = new string[] { fileName };
            GenericProjectUpdate(project, fileNames);
        }
    }
}
