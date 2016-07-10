using System.Configuration;

namespace Catalog.Infrastructure.Contracts.Configuration
{
    public interface ISettingsManager
    {
        T GetConfigurationSection<T>(string sectionName) where T : ConfigurationSection;

        string GetFromAppSettings(string key, bool mandatory = true);

        T? GetFromAppSettings<T>(string key, bool mandatory = true) where T : struct;
    }
}
