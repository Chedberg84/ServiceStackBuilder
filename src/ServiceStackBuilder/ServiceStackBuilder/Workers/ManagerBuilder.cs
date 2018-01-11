using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ServiceStackBuilder.Workers
{
    public class ManagerBuilder : IBuilder
    {
        private ISolution Solution { get; set; }

        public ManagerBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public void Go()
        {
            Console.WriteLine("Building Managers");

            var project = (from p in Solution.Projects where p.Name.ToLower().Contains("manager") select p).FirstOrDefault();

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name, "CRUD");

            //create request and response files
            Directory.CreateDirectory(workingDir);

            string managerTemplatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates\\Manager\\ManagerTemplate.txt");
            string managerTemplate = File.ReadAllText(managerTemplatePath);
            managerTemplate = managerTemplate.Replace("<SolutionName>", UserInput.SolutionName);
            managerTemplate = managerTemplate.Replace("<Name>", UserInput.obj);
            managerTemplate = managerTemplate.Replace("<name>", UserInput.obj);
            string managerFileName = string.Format("{0}{1}", UserInput.obj, "Manager.cs");

            File.WriteAllText(Path.Combine(workingDir, managerFileName), managerTemplate);
            
            //Update the project file to include these 2 new files
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            string projFile = Path.Combine(UserInput.Root, project.Path);
            XDocument doc = XDocument.Load(projFile);

            var itemGroups = doc.Root.Elements(msbuild + "ItemGroup");
            var compile = itemGroups.Where(x => x.Elements(msbuild + "Compile").Count() > 0).FirstOrDefault();

            string managerInclude = "CRUD\\" + managerFileName;

            var existingRequest = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(managerInclude)).FirstOrDefault();
            if (existingRequest == null)
            {
                XElement docRequest = new XElement(msbuild + "Compile", new XAttribute("Include", managerInclude));
                compile.Add(docRequest);
            }

            doc.Save(projFile);
        }
    }
}
