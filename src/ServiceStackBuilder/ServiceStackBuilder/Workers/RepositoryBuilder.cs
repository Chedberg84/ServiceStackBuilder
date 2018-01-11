using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ServiceStackBuilder.Workers
{
    public class RepositoryBuilder : IBuilder
    {
        private ISolution Solution { get; set; }

        public RepositoryBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public void Go()
        {
            Console.WriteLine("Building Repositories");

            var project = (from p in Solution.Projects where p.Name.ToLower().Contains("repository") select p).FirstOrDefault();

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name, "CRUD");

            //create request and response files
            Directory.CreateDirectory(workingDir);

            string repositoryTemplatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates\\Repository\\RepositoryTemplate.txt");
            string repositoryTemplate = File.ReadAllText(repositoryTemplatePath);
            repositoryTemplate = repositoryTemplate.Replace("<SolutionName>", UserInput.SolutionName);
            repositoryTemplate = repositoryTemplate.Replace("<Name>", UserInput.obj);
            repositoryTemplate = repositoryTemplate.Replace("<name>", UserInput.obj);
            string repositoryFileName = string.Format("{0}{1}", UserInput.obj, "Repository.cs");

            File.WriteAllText(Path.Combine(workingDir, repositoryFileName), repositoryTemplate);

            //Update the project file to include these 2 new files
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            string projFile = Path.Combine(UserInput.Root, project.Path);
            XDocument doc = XDocument.Load(projFile);

            var itemGroups = doc.Root.Elements(msbuild + "ItemGroup");
            var compile = itemGroups.Where(x => x.Elements(msbuild + "Compile").Count() > 0).FirstOrDefault();

            string repositoryInclude = "CRUD\\" + repositoryFileName;

            var existingRepository = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(repositoryInclude)).FirstOrDefault();
            if (existingRepository == null)
            {
                XElement docRequest = new XElement(msbuild + "Compile", new XAttribute("Include", repositoryInclude));
                compile.Add(docRequest);
            }

            doc.Save(projFile);
        }
    }
}
