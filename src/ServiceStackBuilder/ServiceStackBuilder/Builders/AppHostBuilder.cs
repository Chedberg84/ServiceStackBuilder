using Onion.SolutionParser.Parser.Model;
using System;
using System.Linq;
using System.Reflection;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Web;

namespace ServiceStackBuilder.Builders
{
    public class AppHostBuilder : BuilderBase
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
            //MethodInfo mi = typeof(AppHost).GetMethod("InitializeContainer");
            //MethodBody mb = mi.GetMethodBody();
            //Console.WriteLine("\r\nMethod: {0}", mi);
        }
    }
}
