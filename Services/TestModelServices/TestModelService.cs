using BasicApiExample.Repository.TestModelRepository;
using Schroders.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.TestModelServices
{
    public class TestModelService : ITestModelService
    {
        private IRepository<TestModel> _repository;

        public TestModelService(IRepository<TestModel> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Create(TestModel model)
        {
            return await _repository.Create(model);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<TestModel> GetModel(int id)
        {
            return await _repository.GetModel(id);
        }

        public async Task<IEnumerable<TestModel>> GetModels()
        {
            return await _repository.GetModels();
        }

        public async Task<bool> Update(TestModel model)
        {
            return await _repository.Update(model);
        }
    }
}
