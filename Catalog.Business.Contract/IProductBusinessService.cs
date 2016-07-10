using Catalog.Model;

namespace Catalog.Business.Contract
{
    public interface IProductBusinessService : IBusinessServiceBase<Product>
    {
        Product GetBySKU(string sku);
    }
}
