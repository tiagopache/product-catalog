using Catalog.Infrastructure.Data.Contexts;
using Catalog.Model.Initialize;
using System.Data.Entity;
using System.Diagnostics;

namespace Catalog.Model.Contexts
{
    public class CatalogDbContext : DbContext, IDbContext
    {
        public IDbSet<Product> Products { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        public CatalogDbContext() : base("CatalogDbContext")
        {
            this.basicContextConfiguration();
        }

        public void Initialize(IDatabaseInitializer<DbContext> databaseInitializer = null)
        {
            Database.SetInitializer(databaseInitializer == null ? new DataSeedingInitializer() : databaseInitializer as IDatabaseInitializer<CatalogDbContext>);
            Migrator.RunMigrations();

            this.Database.Initialize(force: true);
        }

        private void basicContextConfiguration()
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            this.logSql();
        }

        [Conditional("DEBUG")]
        private void logSql()
        {
            this.Database.Log = s => Debug.WriteLine(s);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
