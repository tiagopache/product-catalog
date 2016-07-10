﻿using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.DependencyInjection;
using Catalog.Model.Contexts;
using Catalog.Tests.Common;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace Catalog.Business.Service.Tests
{
    [TestClass]
    public abstract class ServiceTestBase : TestBase
    {
        protected MockRepository mockRepository { get; set; }
        protected static IUnityContainer container { get; set; }
        protected static IDbContext context { get; set; }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            container = UnityConfig.GetConfiguredContainer();

            InjectFactory.SetContainer(container);

            container.RegisterType<IDbContext, CatalogDbContext>(new HierarchicalLifetimeManager());

            UnityHelper.GetRegistrations(container);

            // Initialize Database and Run Migrations
            Database.SetInitializer(new CreateDatabaseIfNotExists<CatalogDbContext>());
            context = container.Resolve<IDbContext>();
            context.Initialize();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            context.Database.Delete();
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }

        [TestInitialize]
        public virtual void Arrange()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);
        }

        [TestCleanup]
        public virtual void CleanUp()
        {

        }
    }
}
