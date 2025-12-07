using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Online_Store.Domain.Contracts;
using Online_Store.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Persistence
{
    public static class SpecificationsEvaluator
    {

        //for EX -> _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P => P.Id == key as int?) as TEntity;

        //GetQuery:- Generate Dynamic Query
        public static IQueryable<TEntity> GetQuery<TKey,TEntity>(IQueryable<TEntity> inputQuery,ISpecifications<TKey, TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery; // _context.Products


            //Check Criteria to filter
            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);  // _context.Products.Where(P => P.Type)
            }


            //check order by to sorting
            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if(spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }


            if(spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

                query = spec.Includes.Aggregate(query, (query, IncludeExpression) => query.Include(IncludeExpression));   // this line to 'include' ,use 'Aggregate' because this operation such as foreach

            return query;



        }
    }
}
