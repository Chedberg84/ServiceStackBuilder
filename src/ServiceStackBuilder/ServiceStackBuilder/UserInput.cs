using Onion.SolutionParser.Parser;
using Onion.SolutionParser.Parser.Model;
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

        public static string SolutionName
        {
            get
            {
                var slnInfo = new FileInfo(sln);
                return slnInfo.Name;
            }
        }
    }
}
