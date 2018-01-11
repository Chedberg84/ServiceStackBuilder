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

            string requestTemplatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates\\CreateRequestTemplate.txt");
            string requestTemplate = File.ReadAllText(requestTemplatePath);
            requestTemplate = requestTemplate.Replace("<RequestTemplate>", UserInput.obj + "Request");
            string requestFileName = string.Format("{0}{1}", UserInput.obj, "Request.cs");

            File.WriteAllText(Path.Combine(workingDir, requestFileName), requestTemplate);

            string responseTemplatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates\\CreateResponseTemplate.txt");
            string responseTemplate = File.ReadAllText(responseTemplatePath);
            responseTemplate = responseTemplate.Replace("<ResponseTemplate>", UserInput.obj + "Response");
            string responseFileName = string.Format("{0}{1}", UserInput.obj, "Response.cs");

            File.WriteAllText(Path.Combine(workingDir, responseFileName), responseTemplate);

            //Update the project file to include these 2 new files
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            string projFile = Path.Combine(UserInput.Root, project.Path);
            XDocument doc = XDocument.Load(projFile);

            var itemGroups = doc.Root.Elements(msbuild + "ItemGroup");
            var compile = itemGroups.Where(x => x.Elements(msbuild + "Compile").Count() > 0).FirstOrDefault();

            string requestInclude = "Messages\\" + UserInput.obj + "\\" + requestFileName;
            string responseInclude = "Messages\\" + UserInput.obj + "\\" + responseFileName;

            var existingRequest = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(requestInclude)).FirstOrDefault();
            if (existingRequest == null)
            {
                XElement docRequest = new XElement(msbuild + "Compile", new XAttribute("Include", requestInclude));
                compile.Add(docRequest);
            }

            var existingResponse = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(responseInclude)).FirstOrDefault();
            if (existingResponse == null)
            {
                XElement docResponse = new XElement(msbuild + "Compile", new XAttribute("Include", responseInclude));
                compile.Add(docResponse);
            }

            doc.Save(projFile);
        }
        
        private void BuildCreate()
        {
        }
        
        private void BuildRead()
        {
        }
        
        private void BuildUpdate()
        {
        }
        
        private void BuildDelete()
        {
        }
    }
}
