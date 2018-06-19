using BasicApiExample.Repository.TestModelRepository;
using Moq;
using NUnit.Framework;
using Schroders.Contracts;
using Services.TestModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.UnitTests
{
    [TestFixture]
    class TestModelServiceTests
    {
        private ITestModelService _service;
        private Mock<IRepository<TestModel>> _repositoryMock;

        [SetUp]
        public void Init()
        {
            _repositoryMock = new Mock<IRepository<TestModel>>();
            _service = new TestModelService(_repositoryMock.Object);
        }

        [Test]
        public async Task Create_AddNewItem_ReturnsTrue()
        {
            _repositoryMock.Setup(r => r.Create(It.IsAny<TestModel>())).Returns(Task.FromResult(true));
            TestModel model = new TestModel
            {
                TestId = 6,
                Name = "Test model 1",
                Created = DateTime.UtcNow.AddDays(-3),
                CreatedBy = "User 1"
            };

            var result = await _service.Create(model);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Create_AddNullItem_ReturnsFalse()
        {
            _repositoryMock.Setup(r => r.Create(It.IsAny<TestModel>())).Returns(Task.FromResult(false));

            var result = await _service.Create(null);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task Create_AddNewItem_RepositoryMethodWasHit()
        {
            TestModel model = new TestModel
            {
                TestId = 6,
                Name = "Test model 1",
                Created = DateTime.UtcNow.AddDays(-3),
                CreatedBy = "User 1"
            };

            var result = await _service.Create(model);

            _repositoryMock.Verify(t => t.Create(It.IsAny<TestModel>()), Times.Once());
        }

        [Test]
        public async Task Delete_PutExistingId_ReturnsTrue()
        {
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.FromResult(true));

            var result = await _service.Delete(3);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Delete_PutNotExistingId_ReturnsFalse()
        {
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.FromResult(false));

            var result = await _service.Delete(45);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task Delete_RemoveItem_RepositoryMethodWasHit()
        {
            TestModel model = new TestModel
            {
                TestId = 6,
                Name = "Test model 1",
                Created = DateTime.UtcNow.AddDays(-3),
                CreatedBy = "User 1"
            };

            var result = await _service.Delete(34);

            _repositoryMock.Verify(t => t.Delete(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public async Task GetModels_GetList_RepositoryMethodWasHit()
        {
            var result = await _service.GetModels();

            _repositoryMock.Verify(t => t.GetModels(), Times.Once());
        }

        [Test]
        public async Task GetModels_GetList_ReturnsList()
        {
            int listCount = 2;
            _repositoryMock.Setup(r => r.GetModels()).Returns(Task.FromResult(
                new List<TestModel>
                {
                    new TestModel {
                        TestId = 1,
                        Name = "Test model 1",
                        Created = DateTime.UtcNow.AddDays(-3),
                        CreatedBy = "User 1"
                    },
                    new TestModel {
                        TestId = 2,
                        Name = "Test model 2",
                        Created = DateTime.UtcNow.AddDays(-2),
                        CreatedBy = "User 1",
                        Updated = DateTime.UtcNow.AddDays(-1)
                    }
                } as IEnumerable<TestModel>)
            );

            var result = await _service.GetModels();

            Assert.AreEqual(listCount, result.Count());
        }

        [Test]
        public async Task GetModel_PutExistingItemId_ReturnsProperItem()
        {
            int searchedId = 1;
            _repositoryMock.Setup(r => r.GetModel(It.IsAny<int>())).Returns(Task.FromResult(
                new TestModel {
                        TestId = 1,
                        Name = "Test model 1",
                        Created = DateTime.UtcNow.AddDays(-3),
                        CreatedBy = "User 1"
                }
            ));

            var result = await _service.GetModel(searchedId);

            Assert.AreEqual(searchedId, result.TestId);
        }

        [Test]
        public async Task Update_PutModifiedModel_ReturnsTrue()
        {
            _repositoryMock.Setup(r => r.Update(It.IsAny<TestModel>())).Returns(Task.FromResult(true));

            var result = await _service.Update(It.IsAny<TestModel>());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Update_PutNullModel_ReturnsFalse()
        {
            _repositoryMock.Setup(r => r.Update(It.IsAny<TestModel>())).Returns(Task.FromResult(false));

            var result = await _service.Update(null);

            Assert.IsFalse(result);
        }
    }
}
