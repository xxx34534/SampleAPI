using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicApiExample.Repository.TestModelRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetModels();
        Task<T> GetModel(int id);
        Task<bool> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(int id);
    }
}