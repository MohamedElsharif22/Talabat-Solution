using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Specifications
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> baseQuery, ISpecifications<TEntity> specs)
        {
            var query = baseQuery;

            if (specs.Criteria is not null)
            {
                query = query.Where(specs.Criteria);
            }

            if (specs.Orderby is not null)
            {
                query = query.OrderBy(specs.Orderby);
            }
            if (specs.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specs.OrderByDesc);
            }

            if (specs.IsPaginationEnabled)
            {
                query = query.Skip(specs.Skip).Take(specs.Take);
            }

            query = specs.Includes.Aggregate(query, (curruntQ, IncludeExp) => curruntQ.Include(IncludeExp));

            return query;
        }
    }
}
