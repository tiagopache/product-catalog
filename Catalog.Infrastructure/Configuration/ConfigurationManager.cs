using Catalog.Infrastructure.Contracts.Configuration;
using System.Configuration;

namespace Catalog.Infrastructure.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        public string AppSettings(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings; }
        }

        public object GetSection(string sectionName)
        {
            return System.Configuration.ConfigurationManager.GetSection(sectionName);
        }
    }
}
