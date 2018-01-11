using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ServiceStackBuilder.Workers
{
    public class ModelBuilder : Builder
    {
        private ISolution Solution { get; set; }

        public ModelBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building Models");

            var project = (from p in Solution.Projects where p.Name.ToLower().Contains("models") select p).FirstOrDefault();

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name, "Messages", UserInput.obj);

            //create request and response files
            Directory.CreateDirectory(workingDir);

            BuildCreate(workingDir, project);
            BuildRead(workingDir, project);
            BuildUpdate(workingDir, project);
            BuildDelete(workingDir, project);
        }
        
        private void BuildCreate(string workingDir, Project project)
        {
            string templatePathRequest = "Templates\\Models\\CreateRequestTemplate.txt";
            string templatePathResponse = "Templates\\Models\\CreateResponseTemplate.txt";
            
            string createRequestFileName = GenericBuild(workingDir, templatePathRequest, "Create", "Request");
            string createResponseFileName = GenericBuild(workingDir, templatePathResponse, "Create", "Response");
            
            string[] fileNames = new string[] { createRequestFileName, createResponseFileName };
            GenericProjectUpdate(project, fileNames);
        }
        
        private void BuildRead(string workingDir, Project project)
        {
            string templatePathRequest = "Templates\\Models\\ReadRequestTemplate.txt";
            string templatePathResponse = "Templates\\Models\\ReadResponseTemplate.txt";
            
            string createRequestFileName = GenericBuild(workingDir, templatePathRequest, "Read", "Request");
            string createResponseFileName = GenericBuild(workingDir, templatePathResponse, "Read", "Response");
            
            string[] fileNames = new string[] { createRequestFileName, createResponseFileName };
            GenericProjectUpdate(project, fileNames);
        }
        
        private void BuildUpdate(string workingDir, Project project)
        {
            string templatePathRequest = "Templates\\Models\\UpdateRequestTemplate.txt";
            string templatePathResponse = "Templates\\Models\\UpdateResponseTemplate.txt";
            
            string createRequestFileName = GenericBuild(workingDir, templatePathRequest, "Update", "Request");
            string createResponseFileName = GenericBuild(workingDir, templatePathResponse, "Update", "Response");
            
            string[] fileNames = new string[] { createRequestFileName, createResponseFileName };
            GenericProjectUpdate(project, fileNames);
        }
        
        private void BuildDelete(string workingDir, Project project)
        {
            string templatePathRequest = "Templates\\Models\\DeleteRequestTemplate.txt";
            string templatePathResponse = "Templates\\Models\\DeleteResponseTemplate.txt";
            
            string createRequestFileName = GenericBuild(workingDir, templatePathRequest, "Delete", "Request");
            string createResponseFileName = GenericBuild(workingDir, templatePathResponse, "Delete", "Response");
            
            string[] fileNames = new string[] { createRequestFileName, createResponseFileName };
            GenericProjectUpdate(project, fileNames);
        }
        
    }
}
