using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;

namespace ServiceStackBuilder.Builders
{
    public class UnitTestBuilder : BuilderBase
    {
        private ISolution Solution { get; set; }

        public UnitTestBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building UnitTests");

            var project = (from p in Solution.Projects where p.Name.ToLower().Contains("unittest") select p).FirstOrDefault();

            //Define the working directory
            string workingDir = Path.Combine(UserInput.TestDir, project.Name);

            Build(workingDir, project);
        }

        private void Build(string workingDir, Project project)
        {
            string templateServicePath = "Templates\\UnitTests\\UnitTestServiceTemplate.txt";
            string templateManagerPath = "Templates\\UnitTests\\UnitTestManagerTemplate.txt";
            string templateRepositoryPath = "Templates\\UnitTests\\UnitTestRepositoryTemplate.txt";

            string serviceFileName = GenericBuild(workingDir, templateServicePath, "", "ServiceTests");
            string managerFileName = GenericBuild(workingDir, templateManagerPath, "", "ManagerTests");
            string repositoryFileName = GenericBuild(workingDir, templateRepositoryPath, "", "RepositoryTests");

            string[] fileNames = new string[] { serviceFileName, managerFileName, repositoryFileName };
            GenericProjectUpdate(project, fileNames);
        }
    }
}
