using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;

namespace Talabat.Core.Specification
{
    public static class SpecificationEvaluation<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery; // db.set<T>()

            if (spec.crateria is not null)
                query = query.Where(spec.crateria); //db.set<T>().where(EX)

            if(spec.Orderby is not null)
                query = query.OrderBy(spec.Orderby);

            else if (spec.OrderbyDecs is not null)
                query = query.OrderByDescending(spec.OrderbyDecs);

            if (spec.IsPagination)
                query = query.Skip(spec.Skip).Take(spec.Take);



            query = spec.includes
                        .Aggregate(query,
                        (CurrentQuery, includesExpression)
                        => CurrentQuery.Include(includesExpression)); //db.set<T>().where(EX).include(EX)

            return query;


        }




    }
}
