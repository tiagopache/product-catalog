namespace Catalog.Infrastructure.ExternalService.Configuration
{
    public interface IEpicomSettings
    {
        string BaseUrl { get; }
        string Key { get; }
        string Password { get; }
        string InitialCatalog { get; }
    }
}
