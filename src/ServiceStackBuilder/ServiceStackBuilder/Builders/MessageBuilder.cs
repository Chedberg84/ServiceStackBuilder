using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;

namespace ServiceStackBuilder.Builders
{
    public class MessageBuilder : BuilderBase
    {
        private ISolution Solution { get; set; }

        public MessageBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building Messages");

            var project = GetProject(Solution, BuilderConstants.Models);

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
            string templatePathRequest = "Templates\\Messages\\CreateRequestTemplate.txt";
            string templatePathResponse = "Templates\\Messages\\CreateResponseTemplate.txt";
            
            string createRequestFileName = GenericBuild(workingDir, templatePathRequest, "Create", "Request");
            string createResponseFileName = GenericBuild(workingDir, templatePathResponse, "Create", "Response");
            
            string[] fileNames = new string[] { createRequestFileName, createResponseFileName };
            GenericProjectUpdate(project, fileNames, "Messages" + "\\" + UserInput.obj);
        }
        
        private void BuildRead(string workingDir, Project project)
        {
            string templatePathRequest = "Templates\\Messages\\ReadRequestTemplate.txt";
            string templatePathResponse = "Templates\\Messages\\ReadResponseTemplate.txt";
            
            string createRequestFileName = GenericBuild(workingDir, templatePathRequest, "Read", "Request");
            string createResponseFileName = GenericBuild(workingDir, templatePathResponse, "Read", "Response");
            
            string[] fileNames = new string[] { createRequestFileName, createResponseFileName };
            GenericProjectUpdate(project, fileNames, "Messages" + "\\" + UserInput.obj);
        }
        
        private void BuildUpdate(string workingDir, Project project)
        {
            string templatePathRequest = "Templates\\Messages\\UpdateRequestTemplate.txt";
            string templatePathResponse = "Templates\\Messages\\UpdateResponseTemplate.txt";
            
            string createRequestFileName = GenericBuild(workingDir, templatePathRequest, "Update", "Request");
            string createResponseFileName = GenericBuild(workingDir, templatePathResponse, "Update", "Response");
            
            string[] fileNames = new string[] { createRequestFileName, createResponseFileName };
            GenericProjectUpdate(project, fileNames, "Messages" + "\\" + UserInput.obj);
        }
        
        private void BuildDelete(string workingDir, Project project)
        {
            string templatePathRequest = "Templates\\Messages\\DeleteRequestTemplate.txt";
            string templatePathResponse = "Templates\\Messages\\DeleteResponseTemplate.txt";
            
            string createRequestFileName = GenericBuild(workingDir, templatePathRequest, "Delete", "Request");
            string createResponseFileName = GenericBuild(workingDir, templatePathResponse, "Delete", "Response");
            
            string[] fileNames = new string[] { createRequestFileName, createResponseFileName };
            GenericProjectUpdate(project, fileNames, "Messages" + "\\" + UserInput.obj);
        }
        
    }
}
