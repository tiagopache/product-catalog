using Catalog.Application.Contract.Contracts;
using Catalog.Application.Contract.ViewModels;
using Catalog.Business.Contract;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Application.Service
{
    public class ProductApplicationService : IProductApplicationService
    {
        [Dependency]
        protected IProductBusinessService ProductBusinessService { get; set; }

        public IList<ProductViewModel> Get(string includeProperties = null)
        {
            return ProductViewModel.Instance.ToContract(this.ProductBusinessService.Get(includeProperties: includeProperties)).ToList();
        }

        public ProductViewModel GetBySKU(string sku)
        {
            return ProductViewModel.Instance.ToContract(this.ProductBusinessService.GetBySKU(sku));
        }

        public ProductViewModel Save(ProductViewModel product)
        {
            return ProductViewModel.Instance.ToContract(this.ProductBusinessService.Save(ProductViewModel.Instance.ToEntity(product)));
        }

        public void Remove(string sku)
        {
            var toRemove = this.ProductBusinessService.GetBySKU(sku);

            if (toRemove != null)
                this.ProductBusinessService.Remove(toRemove);
        }
    }
}
