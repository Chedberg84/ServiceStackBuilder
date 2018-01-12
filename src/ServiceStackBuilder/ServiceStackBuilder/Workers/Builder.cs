using Onion.SolutionParser.Parser.Model;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ServiceStackBuilder.Workers
{
    public abstract class Builder : IBuilder
    {
        public virtual void Go() { }
        
        internal string GenericBuild(string workingDir, string templatePath, string actionVerb, string actionType)
        {
            string templatePathFull = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), templatePath);
            string template = File.ReadAllText(templatePathFull);
            template = template.Replace("<SolutionName>", UserInput.SolutionName);
            string fileName = string.Format("{0}{1}{2}", actionVerb, UserInput.obj, actionType, ".cs");
            File.WriteAllText(Path.Combine(workingDir, fileName), template);

            return fileName;
        }

        internal void GenericProjectUpdate(Project project, string[] fileNames)
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
