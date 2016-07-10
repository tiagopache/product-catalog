using Catalog.Business.Contract;
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

namespace Catalog.Business.Service.Tests
{
    /// <summary>
    /// Summary description for NotificationBusinessServiceTests
    /// </summary>
    [TestClass()]
    public class NotificationBusinessServiceTests : ServiceTestBase
    {
        protected Mock<IRepository<Notification>> mockNotificationRepository { get; set; }
        protected Mock<IUnitOfWork<Notification>> mockUnitOfWork { get; set; }

        protected INotificationBusinessService sut { get; set; }

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            container.RegisterType(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            mockUnitOfWork = container.RegisterMock<IUnitOfWork<Notification>>(mockRepository);
            mockNotificationRepository = container.RegisterMock<IRepository<Notification>>(mockRepository);
            mockUnitOfWork.SetupProperty(u => u.Repository, mockNotificationRepository.Object);

            container.RegisterType<INotificationBusinessService, NotificationBusinessService>();

            sut = InjectFactory.Resolve<INotificationBusinessService>();
        }

        [TestMethod()]
        public void ShouldGetNotificationById()
        {
            // ARRANGE
            var actual = new Notification()
            {
                Id = 1,
                Tipo = "criacao_sku",
                DataEnvio = DateTime.Now,
                IdProduto = 231345,
                IdSku = 200
            };

            mockNotificationRepository.Setup((r) => r.GetById(It.IsAny<int>())).Returns(actual);

            // ACT
            var expected = sut.GetById(actual.Id);

            // ASSERT
            mockRepository.Verify();
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(Notification));
            Assert.AreEqual<Notification>(expected, actual);
        }

        [TestMethod()]
        public void ShouldGetAListOfNotifications()
        {
            // ARRANGE
            var notification1 = new Notification()
            {
                Id = 1,
                Tipo = "criacao_sku",
                DataEnvio = DateTime.Now,
                IdProduto = 231345,
                IdSku = 200,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var notification2 = new Notification()
            {
                Id = 2,
                Tipo = "criacao_sku",
                DataEnvio = DateTime.Now,
                IdProduto = 447781,
                IdSku = 100,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var actual = new List<Notification>();
            actual.Add(notification1);
            actual.Add(notification2);

            mockNotificationRepository.Setup((r) => r.Get(null, null, null)).Returns(actual);

            // ACT
            var expected = sut.Get();

            // ASSERT
            mockRepository.Verify();
            CollectionAssert.AllItemsAreInstancesOfType(expected.ToArray(), typeof(Notification));
            CollectionAssert.AllItemsAreNotNull(expected.ToArray());
            CollectionAssert.AreEqual(expected.ToArray(), actual);
        }

        [TestMethod()]
        public void ShouldSaveANewNotification()
        {
            // ARRANGE
            var actual = new Notification()
            {
                Id = 1,
                Tipo = "criacao_sku",
                DataEnvio = DateTime.Now,
                IdProduto = 231345,
                IdSku = 200,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            mockNotificationRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(default(Notification));
            mockNotificationRepository.Setup(r => r.Insert(It.IsAny<Notification>()));
            mockUnitOfWork.Setup(u => u.Save());

            // ACT
            var expected = sut.Save(actual);

            // ASSERT
            mockRepository.Verify();
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(Notification));
            Assert.AreEqual<Notification>(expected, actual);
        }

        [TestMethod()]
        public void ShouldSaveAnExistentNotification()
        {
            // ARRANGE
            var oldNotification = new Notification()
            {
                Id = 1,
                Tipo = "criacao_sku",
                DataEnvio = DateTime.Now,
                IdProduto = 231345,
                IdSku = 200,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            mockNotificationRepository.Setup(r => r.GetById(oldNotification.Id)).Returns(oldNotification);

            var actual = new Notification()
            {
                Id = 1,
                Tipo = "criacao_sku",
                DataEnvio = DateTime.Now,
                IdProduto = 21164,
                IdSku = 202,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            mockNotificationRepository.Setup(r => r.Update(It.IsAny<Notification>()));

            mockUnitOfWork.Setup(u => u.Save());

            // ACT
            var expected = sut.Save(actual);

            // ASSERT
            mockRepository.Verify();
            Assert.IsNotNull(expected);
            Assert.IsInstanceOfType(expected, typeof(Notification));
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.IdProduto, actual.IdProduto);
            Assert.AreEqual(expected.IdSku, actual.IdSku);
            Assert.AreEqual(expected.Tipo, actual.Tipo);
            Assert.AreEqual(expected.CreatedOn, actual.CreatedOn);
        }

        [TestMethod()]
        public void ShouldRemoveANotification()
        {
            // ARRANGE
            mockNotificationRepository.Setup(r => r.Delete(It.IsAny<int>()));

            mockUnitOfWork.Setup(u => u.Save());

            // ACT
            sut.Remove(1);

            // ASSERT
            mockRepository.Verify();
        }
    }
}
