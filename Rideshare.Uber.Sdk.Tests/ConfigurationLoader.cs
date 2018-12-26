using System.Configuration;
using System.Reflection;

namespace Rideshare.Uber.Sdk.Tests
{
    public static class ConfigurationLoader
    {
        public static Configuration GetConfiguration()
        {
            // TODO: Figure out why config file isn't being loaded in VS xUnit runner AppDomain by default
            var fileMap = new ExeConfigurationFileMap()
            {
                ExeConfigFilename = $"./{Assembly.GetExecutingAssembly().GetName().Name}.dll.config"
            };

            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }
    }
}
