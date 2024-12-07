using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;

namespace Talabat.Core.Specification
{
    public class Specification<T> : ISpecification<T> where T : BaseEntity
    {
        //.Where(p=>p.ID == id)
        public Expression<Func<T, bool>>? crateria { get; set; }
        public List<Expression<Func<T, object>>> includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> Orderby { get; set; }
        public Expression<Func<T, object>> OrderbyDecs { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagination { get; set; }

        public Specification()
        {
            //crateria = null;
        }

        public Specification(Expression<Func<T, bool>> _crateria)
        {
            crateria = _crateria;
        }

        public void AddOrderBy (Expression<Func<T, object>> _)
        {
            Orderby = _;
        }
        
        public void AddOrderByDecs (Expression<Func<T, object>> _)
        {
            OrderbyDecs = _;
        }

        public void ApplyPagination(int skip,int take)
        {
            Skip = skip;
            Take = take;
            IsPagination = true;
        }

    }
}
