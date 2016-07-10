using Catalog.Application.Contract.Contracts;
using Catalog.Application.Contract.ViewModels;
using Catalog.Business.Contract;
using Catalog.Infrastructure.ExternalService.Contract;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Application.Service
{
    public class NotificationApplicationService : INotificationApplicationService
    {
        [Dependency]
        protected INotificationBusinessService NotificationBusinessService { get; set; }

        [Dependency]
        protected IProductBusinessService ProductBusinessService { get; set; }

        [Dependency]
        protected IEpicomService EpicomService { get; set; }

        public IList<NotificationViewModel> Get(string includeProperties = null)
        {
            return NotificationViewModel.Instance.ToContract(this.NotificationBusinessService.Get(includeProperties: includeProperties)).ToList();
        }

        public async Task Initialize()
        {
            var notificationsString = await this.EpicomService.GetInitialCatalog();

            var notifications = JsonConvert.DeserializeObject<IList<NotificationViewModel>>(notificationsString);

            foreach (var notification in notifications)
            {
                await this.Receive(notification);
            }
        }

        public async Task Receive(NotificationViewModel notificationReceived)
        {
            var notificationSaved = this.NotificationBusinessService.Save(NotificationViewModel.Instance.ToEntity(notificationReceived));

            var productString = await this.EpicomService.GetProductByIdAndSKU(notificationSaved.IdProduto, notificationSaved.IdSku);

            var productViewModel = JsonConvert.DeserializeObject<ProductViewModel>(productString);

            var productToSave = ProductViewModel.Instance.ToEntity(productViewModel);

            productToSave.IdParceiro = productViewModel.Id;
            productToSave.Id = 0;

            this.ProductBusinessService.Save(productToSave);
        }
    }
}
