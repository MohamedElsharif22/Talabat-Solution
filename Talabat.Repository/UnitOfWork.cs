using System.Collections;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private readonly Hashtable _repositories;
        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repo = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repo);
                return repo;
            }
            return _repositories[type] as IGenericRepository<TEntity>;
        }
    }
}
