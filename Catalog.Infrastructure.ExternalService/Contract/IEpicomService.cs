using System.Threading.Tasks;

namespace Catalog.Infrastructure.ExternalService.Contract
{
    public interface IEpicomService
    {
        Task<string> GetProductByIdAndSKU(int idProduct, int idSku);

        Task<string> GetInitialCatalog();
    }
}
