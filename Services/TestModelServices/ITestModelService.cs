using Schroders.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.TestModelServices
{
    public interface ITestModelService
    {
        Task<IEnumerable<TestModel>> GetModels();
        Task<TestModel> GetModel(int id);
        Task<bool> Create(TestModel model);
        Task<bool> Update(TestModel model);
        Task<bool> Delete(int id);
    }
}
