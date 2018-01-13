using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ServiceStackBuilder.Builders
{
    public class ManagerBuilder : BuilderBase
    {
        private ISolution Solution { get; set; }

        public ManagerBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building Managers");

            var project = (from p in Solution.Projects where p.Name.ToLower().Contains("manager") select p).FirstOrDefault();

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name, "CRUD");

            //create request and response files
            Directory.CreateDirectory(workingDir);

            Build(workingDir, project);
        }

        private void Build(string workingDir, Project project)
        {
            string templatePath = "Templates\\Manager\\ManagerTemplate.txt";

            string fileName = GenericBuild(workingDir, templatePath, "", "Manager");

            string[] fileNames = new string[] { fileName };
            GenericProjectUpdate(project, fileNames, "CRUD");
        }
    }
}
