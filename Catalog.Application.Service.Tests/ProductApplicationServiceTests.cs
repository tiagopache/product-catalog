using Catalog.Application.Contract.Contracts;
using Catalog.Application.Contract.ViewModels;
using Catalog.Business.Contract;
using Catalog.Infrastructure.DependencyInjection;
using Catalog.Infrastructure.Extensions;
using Catalog.Model;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Application.Service.Tests
{
    [TestClass()]
    public class ProductApplicationServiceTests : ServiceTestBase
    {
        protected Mock<IProductBusinessService> mockProductBusiness { get; set; }

        protected IProductApplicationService sut { get; set; }

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockProductBusiness = container.RegisterMock<IProductBusinessService>(mockRepository);

            container.RegisterType<IProductApplicationService, ProductApplicationService>();

            sut = InjectFactory.Resolve<IProductApplicationService>();
        }

        [TestMethod()]
        public void ApplicationShouldGetAListOfProducts()
        {
            // ARRANGE
            var product1 = new Product()
            {
                Id = 1,
                IdParceiro = 231345,
                SKU = "prd1",
                Nome = "Produto 1",
                Preco = 1759.99m,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var product2 = new Product()
            {
                Id = 2,
                IdParceiro = 447781,
                SKU = "prd2",
                Nome = "Produto 2",
                Preco = 271.99m,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var prods = new List<Product>();
            prods.Add(product1);
            prods.Add(product2);

            var actual = ProductViewModel.Instance.ToContract(prods);

            mockProductBusiness.Setup((r) => r.Get(null, null, null)).Returns(prods);

            // ACT
            var expected = sut.Get();

            // ASSERT
            mockRepository.Verify();
            CollectionAssert.AllItemsAreInstancesOfType(expected.ToArray(), typeof(ProductViewModel));
            CollectionAssert.AllItemsAreNotNull(expected.ToArray());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual.ToArray()[i].Id);
                Assert.AreEqual(expected[i].Nome, actual.ToArray()[i].Nome);
                Assert.AreEqual(expected[i].IdParceiro, actual.ToArray()[i].IdParceiro);
                Assert.AreEqual(expected[i].Codigo, actual.ToArray()[i].Codigo);
                Assert.AreEqual(expected[i].Preco, actual.ToArray()[i].Preco);
                Assert.AreEqual(expected[i].PrecoDe, actual.ToArray()[i].PrecoDe);
                Assert.AreEqual(expected[i].Url, actual.ToArray()[i].Url);
                Assert.AreEqual(expected[i].Ativo, actual.ToArray()[i].Ativo);
            }
        }

        [TestMethod()]
        public void ApplicationShouldGetProductBySKU()
        {
            // ARRANGE
            var product = new Product()
            {
                Id = 1,
                IdParceiro = 231345,
                SKU = "prd1",
                Nome = "Produto 1",
                Preco = 1759.99m,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var actual = ProductViewModel.Instance.ToContract(product);

            mockProductBusiness.Setup(p => p.GetBySKU(product.SKU)).Returns(product);

            // ACT
            var expected = sut.GetBySKU(product.SKU);

            // ASSERT
            mockRepository.Verify();
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(ProductViewModel));
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Nome, actual.Nome);
            Assert.AreEqual(expected.IdParceiro, actual.IdParceiro);
            Assert.AreEqual(expected.Codigo, actual.Codigo);
            Assert.AreEqual(expected.Preco, actual.Preco);
            Assert.AreEqual(expected.PrecoDe, actual.PrecoDe);
            Assert.AreEqual(expected.Url, actual.Url);
            Assert.AreEqual(expected.Ativo, actual.Ativo);
        }
    }
}