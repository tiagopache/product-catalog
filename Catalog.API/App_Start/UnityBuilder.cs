using Catalog.Application.Contract.Contracts;
using Catalog.Application.Service;
using Catalog.Business.Contract;
using Catalog.Business.Service;
using Catalog.Infrastructure.Configuration;
using Catalog.Infrastructure.Contracts.Configuration;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Data.Repositories;
using Catalog.Infrastructure.DependencyInjection;
using Catalog.Infrastructure.ExternalService.Configuration;
using Catalog.Infrastructure.ExternalService.Contract;
using Catalog.Infrastructure.ExternalService.Service;
using Catalog.Model.Contexts;
using Microsoft.Practices.Unity;

namespace Catalog.API.App_Start
{
    public static class UnityBuilder
    {
        public static void Build(IUnityContainer container)
        {
            InjectFactory.SetContainer(container);

            buildContext(container);
            buildInfrastructure(container);
            buildBusinessServices(container);
            buildApplicationServices(container);
            buildExternalServices(container);
        }

        private static void buildContext(IUnityContainer container)
        {
            container.RegisterType<IDbContext, CatalogDbContext>(new HierarchicalLifetimeManager());
        }

        private static void buildApplicationServices(IUnityContainer container)
        {
            container.RegisterType<IProductApplicationService, ProductApplicationService>();
            container.RegisterType<INotificationApplicationService, NotificationApplicationService>();
        }

        private static void buildBusinessServices(IUnityContainer container)
        {
            container.RegisterType<IProductBusinessService, ProductBusinessService>();
            container.RegisterType<INotificationBusinessService, NotificationBusinessService>();
        }

        private static void buildInfrastructure(IUnityContainer container)
        {
            container.RegisterType<IConfigurationManager, ConfigurationManager>();
            container.RegisterType(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }

        private static void buildExternalServices(IUnityContainer container)
        {
            container.RegisterType<IEpicomSettings, EpicomSettings>();
            container.RegisterType<IEpicomService, EpicomService>();
        }
    }
}