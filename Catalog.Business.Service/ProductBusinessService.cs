using Catalog.Business.Contract;
using Catalog.Infrastructure.Extensions;
using Catalog.Model;
using System;
using System.Linq;

namespace Catalog.Business.Service
{
    public class ProductBusinessService : ServiceIdBase<Product>, IProductBusinessService
    {
        public Product GetBySKU(string sku)
        {
            var result = this.UnitOfWork.Repository.Get(p => p.SKU == sku);

            return result.FirstOrDefault();
        }

        public override Product Save(Product toSave)
        {
            var found = this.GetBySKU(toSave.SKU);

            if (found != null)
            {
                var entityKey = found.EntityKey;

                found = toSave.Copy<Product>(found);

                found.UpdatedOn = DateTime.Now;

                this.UnitOfWork.Repository.Update(found);

                toSave = found.Copy<Product>();
            }
            else
                this.UnitOfWork.Repository.Insert(toSave);

            this.UnitOfWork.Save();

            return toSave;
        }
    }
}
