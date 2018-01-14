using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ServiceStackBuilder.Builders
{
    public abstract class BuilderBase : IBuilder
    {
        public virtual void Go() { }
        
        internal string GenericBuild(string workingDir, string templatePath, string fileNamePrefix, string fileNameSuffix)
        {
            string templatePathFull = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), templatePath);
            string template = File.ReadAllText(templatePathFull);

            template = template.Replace("<SolutionName>", UserInput.SolutionName);
            template = template.Replace("<Name>", UserInput.obj);

            string camel = Char.ToLowerInvariant(UserInput.obj[0]) + UserInput.obj.Substring(1);
            template = template.Replace("<name>", Char.ToLowerInvariant(UserInput.obj[0]) + UserInput.obj.Substring(1));

            string fileName = $"{fileNamePrefix}{UserInput.obj}{fileNameSuffix}.cs";
            File.WriteAllText(Path.Combine(workingDir, fileName), template);

            return fileName;
        }

        internal string GenericBuild(string workingDir, string templatePath)
        {
            string templatePathFull = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), templatePath);
            string template = File.ReadAllText(templatePathFull);

            template = template.Replace("<SolutionName>", UserInput.SolutionName);
            template = template.Replace("<Name>", UserInput.obj);

            string camel = Char.ToLowerInvariant(UserInput.obj[0]) + UserInput.obj.Substring(1);
            template = template.Replace("<name>", Char.ToLowerInvariant(UserInput.obj[0]) + UserInput.obj.Substring(1));

            string fileName = $"{UserInput.obj}.cs";
            File.WriteAllText(Path.Combine(workingDir, fileName), template);

            return fileName;
        }

        internal void GenericProjectUpdate(Project project, string[] fileNames, string includeFolder)
        {
            //Update the project file to include these 2 new files
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            string projFile = Path.Combine(UserInput.Root, project.Path);
            XDocument doc = XDocument.Load(projFile);

            var itemGroups = doc.Root.Elements(msbuild + "ItemGroup");
            var compile = itemGroups.Where(x => x.Elements(msbuild + "Compile").Count() > 0).FirstOrDefault();

            foreach(var name in fileNames)
            {
                string include = includeFolder + "\\" + name;

                var existing = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(include)).FirstOrDefault();
                if (existing == null)
                {
                    XElement docRequest = new XElement(msbuild + "Compile", new XAttribute("Include", include));
                    compile.Add(docRequest);
                }
            }

            doc.Save(projFile);
        }

        internal void GenericProjectUpdate(Project project, string[] fileNames)
        {
            //Update the project file to include these 2 new files
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            string projFile = Path.Combine(UserInput.Root, project.Path);
            XDocument doc = XDocument.Load(projFile);

            var itemGroups = doc.Root.Elements(msbuild + "ItemGroup");
            var compile = itemGroups.Where(x => x.Elements(msbuild + "Compile").Count() > 0).FirstOrDefault();

            foreach (var name in fileNames)
            {
                string include = name;

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
