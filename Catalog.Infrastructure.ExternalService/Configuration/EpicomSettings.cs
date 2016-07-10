using Catalog.Infrastructure.Configuration;

namespace Catalog.Infrastructure.ExternalService.Configuration
{
    public class EpicomSettings : SettingsManagerBase, IEpicomSettings
    {
        public string BaseUrl { get { return this.GetFromAppSettings(Constants.Settings.ExternalService.Epicom.BaseUrl); } }

        public string InitialCatalog { get { return this.GetFromAppSettings(Constants.Settings.ExternalService.Epicom.InitialCatalog); } }

        public string Key { get { return this.GetFromAppSettings(Constants.Settings.ExternalService.Epicom.Key); } }

        public string Password { get { return this.GetFromAppSettings(Constants.Settings.ExternalService.Epicom.Password); } }
    }
}
