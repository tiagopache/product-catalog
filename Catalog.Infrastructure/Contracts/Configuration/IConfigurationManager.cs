using System.Configuration;

namespace Catalog.Infrastructure.Contracts.Configuration
{
    public interface IConfigurationManager
    {
        string AppSettings(string key);

        ConnectionStringSettingsCollection ConnectionStrings { get; }

        object GetSection(string sectionName);
    }
}
