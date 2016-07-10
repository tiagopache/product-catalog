using Catalog.Infrastructure.Contracts.Configuration;
using Microsoft.Practices.Unity;
using System;
using System.Configuration;

namespace Catalog.Infrastructure.Configuration
{
    public abstract class SettingsManagerBase : ISettingsManager
    {
        [Dependency]
        protected IConfigurationManager ConfigManager { get; set; }

        public T GetConfigurationSection<T>(string sectionName) where T : ConfigurationSection
        {
            return (T)this.ConfigManager.GetSection(sectionName);
        }

        public string GetFromAppSettings(string key, bool mandatory = true)
        {
            var value = this.ConfigManager.AppSettings(key);

            if (value == null && mandatory)
                throw new Exception(string.Format("The AppSetting keyed '{0}' was not found.", key));

            return value;
        }

        public T? GetFromAppSettings<T>(string key, bool mandatory = true) where T : struct
        {
            var value = this.GetFromAppSettings(key, mandatory);

            try
            {
                var typedValue = Convert.ChangeType(value, typeof(T));

                return (T)typedValue;
            }
            catch
            {
                if (mandatory)
                {
                    throw new Exception(string.Format("The app setting key '{0}' must be a valid '{1}'", key, typeof(T).FullName));
                }

                return default(T);
            }
        }
    }
}
