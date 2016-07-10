using Catalog.Model.Contexts;
using Catalog.Model.Migrations;
using System.Data.Entity;

namespace Catalog.Model.Initialize
{
    public class DataSeedingInitializer : MigrateDatabaseToLatestVersion<CatalogDbContext, CatalogConfiguration> { }
}
