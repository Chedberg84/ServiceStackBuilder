using System.Configuration;

namespace ServiceStackBuilder
{
    public static class BuilderConstants
    {
        public static string Models { get; set; }
        public static string Interfaces { get; set; }
        public static string Service { get; set; }
        public static string Managers { get; set; }
        public static string Repositories { get; set; }
        public static string AATs { get; set; }
        public static string UnitTests { get; set; }

        public static void Init()
        {
            Models = ConfigurationManager.AppSettings["Models"];
            Interfaces = ConfigurationManager.AppSettings["Interfaces"];
            Service = ConfigurationManager.AppSettings["Service"];
            Managers = ConfigurationManager.AppSettings["Managers"];
            Repositories = ConfigurationManager.AppSettings["Repositories"];
            AATs = ConfigurationManager.AppSettings["AATs"];
            UnitTests = ConfigurationManager.AppSettings["UnitTests"];
        }
    }
}
