using BasicApiExample.Controllers;
using Moq;
using NUnit.Framework;
using Schroders.Contracts;
using Services.TestModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace BasicApiExample.UnitTests
{
    [TestFixture]
    class TestModelControllerTests
    {
        private Mock<ITestModelService> _serviceMock;
        private TestModelController _controller;

        [SetUp]
        public void Init()
        {
            _serviceMock = new Mock<ITestModelService>();
            _controller = new TestModelController(_serviceMock.Object);
        }

        [Test]
        public async Task Create_AddNewItem_ReturnsTrue()
        {
            _serviceMock.Setup(r => r.Create(It.IsAny<TestModel>())).Returns(Task.FromResult(true));
            
            var result = await _controller.Create(It.IsAny<TestModel>()) as OkNegotiatedContentResult<bool>;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content);
        }

        [Test]
        public async Task Create_AddNewItem_HitsServiceMethod()
        {
            await _controller.Create(It.IsAny<TestModel>());

            _serviceMock.Verify(t => t.Create(It.IsAny<TestModel>()), Times.Once());
        }

        [Test]
        public async Task GetAll_HitMethod_ReturnList()
        {
            int listCount = 2;
            _serviceMock.Setup(r => r.GetModels()).Returns(Task.FromResult(
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
                } as IEnumerable<TestModel>));

            var result = await _controller.GetAll() as OkNegotiatedContentResult<IEnumerable<TestModel>>;

            Assert.AreEqual(listCount, result.Content.Count());
        }

        [Test]
        public async Task Get_ProperIdProvided_ReturnModel()
        {
            int modelId = 1;
            _serviceMock.Setup(r => r.GetModel(It.IsAny<int>())).Returns(Task.FromResult(
                new TestModel {
                        TestId = 1,
                        Name = "Test model 1",
                        Created = DateTime.UtcNow.AddDays(-3),
                        CreatedBy = "User 1"
                    }));

            var result = await _controller.Get(modelId) as OkNegotiatedContentResult<TestModel>;

            Assert.AreEqual(modelId, result.Content.TestId);
        }

        [Test]
        public async Task Get_InProperIdProvided_ReturnNotFound()
        {
            _serviceMock.Setup(r => r.GetModel(It.IsAny<int>())).Returns(Task.FromResult<TestModel>(null));

            var result = await _controller.Get(It.IsAny<int>());

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Remove_ProperIdProvided_ReturnOkResult()
        {
            _serviceMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.FromResult(true));

            var result = await _controller.Remove(It.IsAny<int>()) as OkNegotiatedContentResult<bool>;

            Assert.IsTrue(result.Content);
        }

        [Test]
        public async Task Remove_InProperIdProvided_ReturnNotFoundResult()
        {
            _serviceMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.FromResult(false));

            var result = await _controller.Remove(It.IsAny<int>());

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Update_ProperModelProvided_ReturnOkResult()
        {
            _serviceMock.Setup(r => r.Update(It.IsAny<TestModel>())).Returns(Task.FromResult(true));

            var result = await _controller.Update(It.IsAny<TestModel>()) as OkNegotiatedContentResult<bool>;

            Assert.IsTrue(result.Content);
        }

        [Test]
        public async Task Update_ProperModelProvided_ReturnNotFoundResult()
        {
            _serviceMock.Setup(r => r.Update(It.IsAny<TestModel>())).Returns(Task.FromResult(false));

            var result = await _controller.Update(It.IsAny<TestModel>());

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
