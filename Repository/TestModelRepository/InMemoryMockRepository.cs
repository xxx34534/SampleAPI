using Schroders.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicApiExample.Repository.TestModelRepository
{
    public class InMemoryMockRepository : IRepository<TestModel>
    {
        private IList<TestModel> _dbContext = new List<TestModel>
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
                },

                new TestModel {
                    TestId = 3,
                    Name = "Test model 3",
                    Created = DateTime.UtcNow.AddDays(-1),
                    CreatedBy = "User 2",
                    Updated = DateTime.UtcNow
                }
            };
        public async Task<bool> Create(TestModel model)
        {
            _dbContext.Add(model);

            return await Task.FromResult(true);
        }

        public async Task<bool> Delete(int id)
        {
            var model = _dbContext.FirstOrDefault(entity => entity.TestId == id);
            return await Task.FromResult(_dbContext.Remove(model));
        }

        public async Task<TestModel> GetModel(int id)
        {
            return await Task.FromResult(_dbContext.FirstOrDefault(model => model.TestId == id));
        }

        public async Task<IEnumerable<TestModel>> GetModels()
        {
            return await Task.FromResult(_dbContext);
        }

        public async Task<bool> Update(TestModel model)
        {
            var modelToModify = _dbContext.FirstOrDefault(entity => entity.TestId == model.TestId);

            if (modelToModify == null)
                return false;

            modelToModify.Name = model.Name;
            modelToModify.Created = model.Created;
            modelToModify.CreatedBy = model.CreatedBy;
            modelToModify.Updated = DateTime.UtcNow;
            modelToModify.UpdatedBy = "user2";

            return await Task.FromResult(true); ;
        }
    }
}