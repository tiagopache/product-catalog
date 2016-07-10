using Catalog.Application.Contract.Contracts;
using Catalog.Application.Contract.ViewModels;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Catalog.API.Controllers
{
    public class NotificationController : ApiController
    {
        [Dependency]
        protected INotificationApplicationService NotificationApplicationService { get; set; }

        // GET api/<controller>
        /// <summary>
        /// Recupera as notificações recebidas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NotificationViewModel> Get()
        {
            return this.NotificationApplicationService.Get();
        }

        /// <summary>
        /// Inicializa o sistema com os dados do parceiro (Epicom)
        /// </summary>
        [Route("api/notification/initialize")]
        public async Task GetInitialize()
        {
            await this.NotificationApplicationService.Initialize();
        }

        // POST api/<controller>
        /// <summary>
        /// Recebe a notificação enviada pelo parceiro (Epicom)
        /// </summary>
        /// <param name="value">In <see cref="NotificationViewModel"/> format</param>
        public void Post([FromBody]NotificationViewModel value)
        {
            this.NotificationApplicationService.Receive(value);
        }
    }
}