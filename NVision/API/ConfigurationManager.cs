using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace API
{
    public static class ConfigurationManager
    {
        private static IConfiguration AppSetting { get; }
        private static bool IsDevelopmentEnv { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            IsDevelopmentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        }

        public static string GetKey()
        {
            return IsDevelopmentEnv ? AppSetting["JWS-Dev:key"] : AppSetting["JWS-Prod:key"];
        }

        public static string GetIssuer()
        {
            return IsDevelopmentEnv ? AppSetting["JWS-Dev:issuerAddress"] : AppSetting["JWS-Prod:issuerAddress"];
        }

        public static string GetAudience()
        {
            return IsDevelopmentEnv ? AppSetting["JWS-Dev:audienceAddress"] : AppSetting["JWS-Prod:audienceAddress"];
        }
    }

}
