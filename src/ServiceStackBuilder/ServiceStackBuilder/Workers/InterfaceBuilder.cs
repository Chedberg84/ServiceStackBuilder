using Onion.SolutionParser.Parser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceStackBuilder.Workers
{
    public class InterfaceBuilder : Builder
    {
        private ISolution Solution { get; set; }

        public InterfaceBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building Interfaces");

            var project = (from p in Solution.Projects where p.Name.ToLower().Contains("interface") select p).FirstOrDefault();

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name, UserInput.obj);
            Directory.CreateDirectory(workingDir);

            //create files
            string managerTemplatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates\\Manager\\ManagerInterfaceTemplate.txt");
            string managerTemplate = File.ReadAllText(managerTemplatePath);
            managerTemplate = managerTemplate.Replace("<SolutionName>", UserInput.SolutionName);
            managerTemplate = managerTemplate.Replace("<Name>", UserInput.obj);
            managerTemplate = managerTemplate.Replace("<name>", UserInput.obj);
            string managerFileName = string.Format("I{0}{1}", UserInput.obj, "Manager.cs");

            File.WriteAllText(Path.Combine(workingDir, managerFileName), managerTemplate);

            string repositoryTemplatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates\\Repository\\RepositoryInterfaceTemplate.txt");
            string repositoryTemplate = File.ReadAllText(repositoryTemplatePath);
            repositoryTemplate = repositoryTemplate.Replace("<SolutionName>", UserInput.SolutionName);
            repositoryTemplate = repositoryTemplate.Replace("<Name>", UserInput.obj);
            repositoryTemplate = repositoryTemplate.Replace("<name>", UserInput.obj);
            string repositoryFileName = string.Format("I{0}{1}", UserInput.obj, "Repository.cs");

            File.WriteAllText(Path.Combine(workingDir, repositoryFileName), repositoryTemplate);

            //Update the project file to include these 2 new files
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            string projFile = Path.Combine(UserInput.Root, project.Path);
            XDocument doc = XDocument.Load(projFile);

            var itemGroups = doc.Root.Elements(msbuild + "ItemGroup");
            var compile = itemGroups.Where(x => x.Elements(msbuild + "Compile").Count() > 0).FirstOrDefault();

            string managerInclude = UserInput.obj + "\\" + managerFileName;
            string repositoryInclude = UserInput.obj + "\\" + repositoryFileName;

            var existingRequest = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(managerInclude)).FirstOrDefault();
            if (existingRequest == null)
            {
                XElement docRequest = new XElement(msbuild + "Compile", new XAttribute("Include", managerInclude));
                compile.Add(docRequest);
            }

            var existingResponse = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(repositoryInclude)).FirstOrDefault();
            if (existingResponse == null)
            {
                XElement docResponse = new XElement(msbuild + "Compile", new XAttribute("Include", repositoryInclude));
                compile.Add(docResponse);
            }

            doc.Save(projFile);
        }
    }
}
