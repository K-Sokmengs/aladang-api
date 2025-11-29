using System;
namespace aladang_server_api.Models.AppAuthorize
{
    static class ConfigurationManagerApp
    {
        public static IConfiguration AppSetting
        {
            get;
        }
        static ConfigurationManagerApp()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}

