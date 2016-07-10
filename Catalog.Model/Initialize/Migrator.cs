using Catalog.Model.Migrations;
using System.Data.Entity.Migrations;

namespace Catalog.Model.Initialize
{
    public class Migrator
    {
        public static void RunMigrations()
        {
            var migrationConfigurations = new CatalogConfiguration();
            var dbMigrator = new DbMigrator(migrationConfigurations);

            dbMigrator.Update();
        }
    }
}
