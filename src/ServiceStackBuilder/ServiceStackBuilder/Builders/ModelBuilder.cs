﻿using Onion.SolutionParser.Parser.Model;
using System;
using System.IO;
using System.Linq;

namespace ServiceStackBuilder.Builders
{
    public class ModelBuilder : BuilderBase
    {
        private ISolution Solution { get; set; }

        public ModelBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building Models");

            var project = (from p in Solution.Projects where p.Name.ToLower().Contains("models") select p).FirstOrDefault();

            //Define the working directory
            string workingDir = Path.Combine(UserInput.Root, project.Name, "Models");

            //create request and response files
            Directory.CreateDirectory(workingDir);

            BuildModel(workingDir, project);
        }
        
        private void BuildModel(string workingDir, Project project)
        {
            string templatePath = "Templates\\Models\\ModelTemplate.txt";
            
            string fileName = GenericBuild(workingDir, templatePath);
            
            string[] fileNames = new string[] { fileName };
            GenericProjectUpdate(project, fileNames, "Models");
        }
    }
}
