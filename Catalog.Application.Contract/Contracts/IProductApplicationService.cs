using Catalog.Application.Contract.ViewModels;

namespace Catalog.Application.Contract.Contracts
{
    public interface IProductApplicationService : IApplicationServiceBase<ProductViewModel>
    {
        ProductViewModel GetBySKU(string sku);

        ProductViewModel Save(ProductViewModel product);

        void Remove(string sku);
    }
}
