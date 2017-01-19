using System.Configuration;

namespace JinnSports.Parser.App
{
    public static class ConfigSettings
    {
        public static string GetPath(string section, string name)
        {
            var config = ConfigurationManager.GetSection(section) as MyConfigSection;
            return config.Instances[name].Path;
        }

        public static int GetAsyncInterval(string section, string name)
        {
            var config = ConfigurationManager.GetSection(section) as MyConfigSection;
            return config.Instances[name].AsyncInterval;
        }

        public static int GetCooldown(string section, string name)
        {
            var config = ConfigurationManager.GetSection(section) as MyConfigSection;
            return config.Instances[name].Cooldown;
        }

        public static int GetTimeout(string section, string name)
        {
            var config = ConfigurationManager.GetSection(section) as MyConfigSection;
            return config.Instances[name].Timeout;
        }
    }
}