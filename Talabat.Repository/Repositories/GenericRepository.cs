using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;
using Talabat.Repository.Specifications;

namespace Talabat.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        #region Using Lazy Loading For Getting Navigational Properties
        // Using Lazy Loading For Getting Navigational Properties
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        #endregion


        // Used Specification Design Pattern
        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await SpecificationEvaluator<T>.BuildQuery(_context.Set<T>(), spec).ToListAsync();
        }



        // Used Specification Design Pattern
        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        {
            return await SpecificationEvaluator<T>.BuildQuery(_context.Set<T>(), spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCount(ISpecifications<T> spec)
        {
            return await SpecificationEvaluator<T>.BuildQuery(_context.Set<T>(), spec).CountAsync();
        }

        public async Task AddAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        public void Delete(T entity)
            => _context.Remove(entity);
    }
}
