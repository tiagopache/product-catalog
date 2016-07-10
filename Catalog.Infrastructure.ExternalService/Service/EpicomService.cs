using Catalog.Infrastructure.ExternalService.Configuration;
using Catalog.Infrastructure.ExternalService.Contract;
using Microsoft.Practices.Unity;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.ExternalService.Service
{
    public class EpicomService : IEpicomService
    {
        [Dependency]
        protected IEpicomSettings EpicomSettings { get; set; }

        public async Task<string> GetInitialCatalog()
        {
            using (var webClient = new WebClient())
            {
                var initialCatalog = await webClient.DownloadStringTaskAsync(this.EpicomSettings.InitialCatalog);

                return initialCatalog;
            }
        }

        public async Task<string> GetProductByIdAndSKU(int idProduct, int idSku)
        {
            using (var webClient = new WebClient())
            {
                var baseUrl = this.EpicomSettings.BaseUrl;
                var resource = $"marketplace/produtos/{idProduct}/skus/{idSku}";
                var key = this.EpicomSettings.Key;
                var password = this.EpicomSettings.Password;

                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{key}:{password}"));
                webClient.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";

                var productString = await webClient.DownloadStringTaskAsync($"{baseUrl}{resource}");

                return productString;
            }
        }
    }
}
