﻿using System.IO;

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
    }
}
