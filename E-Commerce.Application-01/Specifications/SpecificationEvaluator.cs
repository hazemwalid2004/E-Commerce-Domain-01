using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application_01.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery,ISpecifications<TEntity, TKey> Spec)where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if (Spec.Criteria != null)
            {
                query = query.Where(Spec.Criteria);
            }

            if (Spec.IncludeExpressions.Any())
            {
                query = Spec.IncludeExpressions.Aggregate(query, (Current, NextExp) => Current.Include(NextExp));
            }

            return query;
        }
    }
}
