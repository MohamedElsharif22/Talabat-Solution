using Talabat.Core.Entities;
using Talabat.Core.Repositories.Interfaces;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> CompleteAsync();
    }
}
