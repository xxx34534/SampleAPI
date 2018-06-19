using BasicApiExample.Repository.TestModelRepository;
using NUnit.Framework;
using Schroders.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.UnitTests
{
    [TestFixture]
    class InMemoryMockRepositoryTests
    {
        private IRepository<TestModel> _repository;

        [SetUp]
        public void Init()
        {
            _repository = new InMemoryMockRepository();
        }

        [Test]
        public async Task Create_PassProperModel_ReturnTrue()
        {
            TestModel model = new TestModel
            {
                TestId = 6,
                Name = "Test model 1",
                Created = DateTime.UtcNow.AddDays(-3),
                CreatedBy = "User 1"
            };

            var result = await _repository.Create(model);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Delete_ExistingIdPassed_ReturnTrue()
        {
            int existingId = 2;

            var result = await _repository.Delete(existingId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Delete_NotExistingIdPassed_ReturnFalse()
        {
            int nonExistingId = 5;

            var result = await _repository.Delete(nonExistingId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetModels_CallMethod_ReturnList()
        {
            int listCount = 3;

            var result = await _repository.GetModels();

            Assert.AreEqual(listCount, result.Count());
        }

        [Test]
        public async Task GetModel_PassExistingId_ReturnModel()
        {
            int existingId = 2;

            var result = await _repository.GetModel(existingId);

            Assert.IsNotNull(result);
            Assert.AreEqual(existingId, result.TestId);
        }

        [Test]
        public async Task GetModel_PassNonExistingId_ReturnNull()
        {
            int existingId = 5;

            var result = await _repository.GetModel(existingId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task Update_PassInProperModel_ReturnFalse()
        {
            TestModel updateModel = new TestModel
            {
                TestId = 6,
                Name = "Test model 1",
                Created = DateTime.UtcNow.AddDays(-3),
                CreatedBy = "User 1"
            };

            var result = await _repository.Update(updateModel);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task Update_PassProperModel_ReturnTrue()
        {
            TestModel updateModel = new TestModel
            {
                TestId = 2,
                Name = "Test model 1",
                Created = DateTime.UtcNow.AddDays(-3),
                CreatedBy = "User 1"
            };

            var result = await _repository.Update(updateModel);

            Assert.IsTrue(result);
        }
    }
}
