using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SringHelp
{
    public static class ConfigurationManager
    {
        private static IConfigurationRoot Configuration { get; set; }

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public static string GetSection(string sectionName)
        {
            return Configuration[sectionName];
        }
    }
}
