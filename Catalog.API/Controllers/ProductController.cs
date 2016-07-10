using Catalog.Application.Contract.Contracts;
using Catalog.Application.Contract.ViewModels;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Catalog.API.Controllers
{
    public class ProductController : ApiController
    {
        [Dependency]
        protected IProductApplicationService ProductApplicationService { get; set; }

        // GET api/<controller>
        /// <summary>
        /// Recupera uma lista com os produtos do catálogo
        /// </summary>
        /// <returns>Um <see cref="IEnumerable{ProductViewModel}"/> de produtos</returns>
        public IEnumerable<ProductViewModel> Get()
        {
            return this.ProductApplicationService.Get();
        }

        /// <summary>
        /// Recupera uma lista de produtos dentro da faixa de preços solicitada
        /// </summary>
        /// <param name="init"></param>
        /// <param name="end"></param>
        /// <returns>Um <see cref="IEnumerable{ProductViewModel}"/> de produtos</returns>
        [Route("api/product/pricerange/{init}/{end}")]
        public IEnumerable<ProductViewModel> GetPriceRange([FromUri]decimal init, [FromUri]decimal end)
        {
            return this.ProductApplicationService.Get().Where(p => p.Preco >= init && p.Preco <= end).OrderBy(p => p.Preco);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Recupera um produto pelo SKU
        /// </summary>
        /// <param name="sku">SKU do produto</param>
        /// <returns>Produto relacionado ao SKU</returns>
        [Route("api/product/sku/{sku}")]
        public ProductViewModel GetBySKU(string sku)
        {
            return this.ProductApplicationService.GetBySKU(sku);
        }

        // POST api/<controller>
        /// <summary>
        /// Insere um novo produto no catálogo
        /// </summary>
        /// <param name="value">Produto a ser inserido</param>
        /// <returns>Produto inserido</returns>
        public ProductViewModel Post([FromBody]ProductViewModel value)
        {
            return this.ProductApplicationService.Save(value);
        }

        // PUT api/<controller>
        /// <summary>
        /// Atualiza um produto do catálogo
        /// </summary>
        /// <param name="value">Produto com os dados a serem atualizados</param>
        /// <returns>Produto atualizado</returns>
        public ProductViewModel Put([FromBody]ProductViewModel value)
        {
            return this.ProductApplicationService.Save(value);
        }

        // DELETE api/<controller>/SKUXYZ
        /// <summary>
        /// Remove um produto do catálogo
        /// </summary>
        /// <param name="sku">SKU do produto</param>
        public void Delete(string sku)
        {
            this.ProductApplicationService.Remove(sku);
        }
    }
}