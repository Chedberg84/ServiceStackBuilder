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
    public class AppHostBuilder : Builder
    {
        private ISolution Solution { get; set; }

        public AppHostBuilder(ISolution solution)
        {
            Solution = solution;
        }

        public override void Go()
        {
            Console.WriteLine("Building AppHost");

            var project = (from p in Solution.Projects where p.Name.ToLower().Contains("???") select p).FirstOrDefault();
            
            //read the app host file and add some new lines to it.
            MethodInfo mi = typeof(AppHost).GetMethod("InitializeContainer");
            MethodBody mb = mi.GetMethodBody();
            Console.WriteLine("\r\nMethod: {0}", mi);
        }
    }
}
