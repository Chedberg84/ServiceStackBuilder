using System.IO;

namespace ServiceStackBuilder
{
    public static class UserInput
    {
        public static string sln = @"C:\GitHub\CostsApi\src\CostsApi.sln";
        public static string obj = "BadAss";

        public static string Root
        {
            get
            {
                var slnInfo = new FileInfo(sln);
                return slnInfo.DirectoryName;
            }
        }

        public static string TestDir
        {
            get
            {
                var slnInfo = new FileInfo(sln);
                return Path.Combine(slnInfo.Directory.Parent.FullName, "tests");
            }
        }

        public static string SolutionName
        {
            get
            {
                var slnInfo = new FileInfo(sln);
                var name = slnInfo.Name.Substring(0, slnInfo.Name.Length - 4);
                return name;
            }
        }
    }
}
