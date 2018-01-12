using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ServiceStackBuilder.Workers
{
    public class ServiceBuilder : Builder
    {
        private ISolution Solution { get; set; }

        public ServiceBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building Service");

            var project = (from p in Solution.Projects where p.Name.ToLower().Contains("servicedefinition") select p).FirstOrDefault();

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name);

            //create request and response files
            //Directory.CreateDirectory(workingDir);

            string templatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates\\Service\\ServiceTemplate.txt");
            string template = File.ReadAllText(templatePath);
            template = template.Replace("<SolutionName>", UserInput.SolutionName);
            template = template.Replace("<Name>", UserInput.obj);
            template = template.Replace("<name>", UserInput.obj);
            string fileName = string.Format("{0}{1}", UserInput.obj, "Service.cs");

            File.WriteAllText(Path.Combine(workingDir, fileName), template);

            //Update the project file to include these 2 new files
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            string projFile = Path.Combine(UserInput.Root, project.Path);
            XDocument doc = XDocument.Load(projFile);

            var itemGroups = doc.Root.Elements(msbuild + "ItemGroup");
            var compile = itemGroups.Where(x => x.Elements(msbuild + "Compile").Count() > 0).FirstOrDefault();

            string include = fileName;

            var existing = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(include)).FirstOrDefault();
            if (existing == null)
            {
                XElement docRequest = new XElement(msbuild + "Compile", new XAttribute("Include", include));
                compile.Add(docRequest);
            }

            doc.Save(projFile);
        }
    }
}
