using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ServiceStackBuilder.Workers
{
    public class ModelBuilder : IBuilder
    {
        private ISolution Solution { get; set; }

        public ModelBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public void Go()
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
        
        private string GenericBuild(string workingDir, string templatePath, string actionVerb, string actionType)
        {
            string templatePathFull = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), templatePath);
            string template = File.ReadAllText(templatePathFull);
            template = template.Replace("<SolutionName>", UserInput.SolutionName);
            string fileName = string.Format("{0}{1}{2}", actionVerb, UserInput.obj, actionType, ".cs");
            File.WriteAllText(Path.Combine(workingDir, fileName), template);
            
            return fileName;
        }
        
        private void GenericProjectUpdate(Project project, string[] fileNames)
        {
            //Update the project file to include these 2 new files
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            string projFile = Path.Combine(UserInput.Root, project.Path);
            XDocument doc = XDocument.Load(projFile);

            var itemGroups = doc.Root.Elements(msbuild + "ItemGroup");
            var compile = itemGroups.Where(x => x.Elements(msbuild + "Compile").Count() > 0).FirstOrDefault();

            foreach(var name in fileNames)
            {
                string include = "Messages\\" + UserInput.obj + "\\" + name;

                var existing = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(include)).FirstOrDefault();
                if (existing == null)
                {
                    XElement docRequest = new XElement(msbuild + "Compile", new XAttribute("Include", include));
                    compile.Add(docRequest);
                }
            }

            doc.Save(projFile);
        }
        
    }
}
