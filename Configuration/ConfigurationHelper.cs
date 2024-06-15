using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace WordPadWindowsAutomation.Configuration
{
    public static class ConfigurationHelper
    {
        private static dynamic _config;

        static ConfigurationHelper()
        {
            LoadConfiguration();
        }

        private static void LoadConfiguration()
        {
            string json = File.ReadAllText("config.json");
            _config = JsonConvert.DeserializeObject(json);
        }

        public static string GetBaseUrl()
        {
            return $"{_config.base_url}";
        }
    }
}