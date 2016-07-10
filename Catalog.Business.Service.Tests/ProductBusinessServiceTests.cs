﻿using Catalog.Business.Contract;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.Repositories;
using Catalog.Infrastructure.DependencyInjection;
using Catalog.Infrastructure.Extensions;
using Catalog.Model;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Catalog.Business.Service.Tests
{
    [TestClass()]
    public class ProductBusinessServiceTests : ServiceTestBase
    {
        protected Mock<IRepository<Product>> mockProductRepository { get; set; }
        protected Mock<IUnitOfWork<Product>> mockUnitOfWork { get; set; }

        protected IProductBusinessService sut { get; set; }

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            container.RegisterType(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            mockUnitOfWork = container.RegisterMock<IUnitOfWork<Product>>(mockRepository);
            mockProductRepository = container.RegisterMock<IRepository<Product>>(mockRepository);
            mockUnitOfWork.SetupProperty(u => u.Repository, mockProductRepository.Object);

            container.RegisterType<IProductBusinessService, ProductBusinessService>();

            sut = InjectFactory.Resolve<IProductBusinessService>();
        }

        [TestMethod()]
        public void ShouldGetProductById()
        {
            // ARRANGE
            var actual = new Product()
            {
                Id = 1,
                IdParceiro = 231345,
                SKU = "prd1",
                Nome = "Produto 1",
                Preco = 1759.99m
            };

            mockProductRepository.Setup((r) => r.GetById(It.IsAny<int>())).Returns(actual);

            // ACT
            var expected = sut.GetById(actual.Id);

            // ASSERT
            mockRepository.Verify();
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(Product));
            Assert.AreEqual<Product>(expected, actual);
        }

        [TestMethod()]
        public void ShouldGetAListOfProducts()
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

            var actual = new List<Product>();
            actual.Add(product1);
            actual.Add(product2);

            mockProductRepository.Setup((r) => r.Get(null, null, null)).Returns(actual);

            // ACT
            var expected = sut.Get();

            // ASSERT
            mockRepository.Verify();
            CollectionAssert.AllItemsAreInstancesOfType(expected.ToArray(), typeof(Product));
            CollectionAssert.AllItemsAreNotNull(expected.ToArray());
            CollectionAssert.AreEqual(expected.ToArray(), actual);
        }

        [TestMethod()]
        public void ShouldSaveANewProduct()
        {
            // ARRANGE
            var actual = new Product()
            {
                Id = 1,
                IdParceiro = 231345,
                SKU = "prd1",
                Nome = "Produto 1",
                Preco = 1759.99m
            };

            mockProductRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(default(Product));
            mockProductRepository.Setup(r => r.Insert(It.IsAny<Product>()));
            mockUnitOfWork.Setup(u => u.Save());

            // ACT
            var expected = sut.Save(actual);

            // ASSERT
            mockRepository.Verify();
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(Product));
            Assert.AreEqual<Product>(expected, actual);
        }

        [TestMethod()]
        public void ShouldSaveAnExistentProduct()
        {
            // ARRANGE
            var oldProduct = new Product()
            {
                Id = 1,
                IdParceiro = 231345,
                SKU = "prd1",
                Nome = "Produto 1",
                Preco = 1759.99m,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            mockProductRepository.Setup(r => r.GetById(oldProduct.Id)).Returns(oldProduct);

            var actual = new Product()
            {
                Id = 1,
                IdParceiro = 231345,
                SKU = "prd1",
                Nome = "Produto 1",
                Preco = 5279.99m
            };
            mockProductRepository.Setup(r => r.Update(It.IsAny<Product>()));

            mockUnitOfWork.Setup(u => u.Save());

            // ACT
            var expected = sut.Save(actual);

            // ASSERT
            mockRepository.Verify();
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(Product));
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Nome, actual.Nome);
            Assert.AreEqual(expected.Preco, actual.Preco);
            Assert.AreEqual(expected.SKU, actual.SKU);
            Assert.AreEqual(expected.CreatedOn, actual.CreatedOn);
        }

        [TestMethod()]
        public void ShouldRemoveAProduct()
        {
            // ARRANGE
            mockProductRepository.Setup(r => r.Delete(It.IsAny<int>()));

            mockUnitOfWork.Setup(u => u.Save());

            // ACT
            sut.Remove(1);

            // ASSERT
            mockRepository.Verify();
        }

        [TestMethod()]
        public void ShouldGetProductBySKU()
        {
            // ARRANGE
            var product2 = new Product()
            {
                Id = 2,
                SKU = "prd2",
                Nome = "Produto 2",
                Preco = 326.99m,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var actual = new List<Product>();
            actual.Add(product2);

            mockProductRepository.Setup((r) => r.Get(It.IsAny<Expression<Func<Product, bool>>>(), null, null)).Returns(actual);

            // ACT
            var expected = sut.GetBySKU(product2.SKU);

            // ASSERT
            mockRepository.Verify();
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(Product));
            Assert.AreEqual<Product>(expected, product2);
        }
    }
}