using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        // Works With LazyLoading
        Task<T?> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        // Works With Specification DP
        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);

        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

        Task<int> GetCount(ISpecifications<T> spec);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
