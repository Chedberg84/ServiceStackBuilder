using System;
using Onion.SolutionParser.Parser;
using System.Linq;
using Onion.SolutionParser.Parser.Model;
using System.IO;
using System.Xml.Linq;

namespace ServiceStackBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Welcome to the ServiceStack Builder console application.");
            //Console.WriteLine("Location of sln file?");
            //sln = Console.ReadLine();

            //Console.WriteLine("New request object name.");
            //obj = Console.ReadLine();

            //VERIFY THE USER INPUT HERE!!
            //var slnInfo = new FileInfo(UserInput.sln);

            Console.WriteLine("Would you like me to get started?");
            SayPlease();

            Console.WriteLine("Processing...");

            Process();

            Console.WriteLine("Done doing your work for you. You're welcome.");
            Console.ReadLine();
        }

        static bool SayPlease()
        {
            var please = Console.ReadLine();

            bool sayPlease = false;
            while(sayPlease == false)
            {
                if(please.ToLower().Equals("no"))
                {
                    Environment.Exit(0);
                }
                else if(please.ToLower().Contains("yes please"))
                {
                    sayPlease = true;
                }
                else
                {
                    Console.WriteLine("Na ah ah, you didn't say the magic word.");
                    please = Console.ReadLine();
                }
            }
            
            return true;
        }

        static void Process()
        {
            //figure out where we are in the sln file.
            var solution = SolutionParser.Parse(UserInput.sln);
            var projects = solution.Projects;

            var modelsProject = (from p in projects where p.Name.ToLower().Contains("models") select p).FirstOrDefault();
            
            //Find the modles project
            BuildModles(modelsProject);
            //create a new folder for this request object
            //Create request
            //Create response
            //udpate the modles project file


            //Find the interfaces project
            //add new Manager interface
            //add new Repository interface
            //update interface project file


            //Find the service deffinition
            //Create a new service class with CRUD methods for this request object
            //update service deffinition project file


            //Find the managers project
            //add a new manager file
            //update the managers project file

            //Find the repository project
            //add a new repository file
            //update repository project

            //Find the apphost file (or container manager file)
            //update this file with new interface mappings
            
            //DONE
        }

        static void BuildModles(Project project)
        {
            Console.WriteLine("Building Models");

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name, "Messages", UserInput.obj);

            //create request and response files
            Directory.CreateDirectory(workingDir);
            
            string requestFileName = string.Format("{0}{1}", UserInput.obj, "Request.cs");
            File.Create(Path.Combine(workingDir, requestFileName));

            string responseFileName = string.Format("{0}{1}", UserInput.obj, "Response.cs");
            File.Create(Path.Combine(workingDir, responseFileName));

            //Update the project file to include these 2 new files
            string projFile = Path.Combine(UserInput.Root, project.Path);
            XDocument doc = XDocument.Load(projFile);
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            
            var itemGroups = doc.Root.Elements(msbuild + "ItemGroup");
            var compile = itemGroups.Where(x => x.Elements(msbuild + "Compile").Count() > 0).FirstOrDefault();

            string requestInclude = "Messages\\" + UserInput.obj + "\\" + requestFileName;
            string responseInclude = "Messages\\" + UserInput.obj + "\\" + responseFileName;

            var existingRequest = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(requestInclude)).FirstOrDefault();
            if(existingRequest == null)
            {
                XElement docRequest = new XElement(msbuild + "Compile", new XAttribute("Include", requestInclude));
                compile.Add(docRequest);
            }

            var existingResponse = compile.Elements().Where(x => x.FirstAttribute.Value.Equals(responseInclude)).FirstOrDefault();
            if(existingResponse == null)
            {
                XElement docResponse = new XElement(msbuild + "Compile", new XAttribute("Include", responseInclude));
                compile.Add(docResponse);
            }

            doc.Save(projFile);
            
        }
    }
}
