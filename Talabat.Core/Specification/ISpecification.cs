using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        //db.Set<Product>().Where(p=>p.ID == id).Include(p=>p.productBrand).Include(p=>p.productCategory)


        //.Where(p=>p.ID == id)
        public Expression<Func<T,bool>>? crateria { get; set; }

        //.Include(p=>p.productBrand).Include(p=>p.productCategory)
        public List<Expression<Func<T,object>>> includes { get; set; }

        public Expression<Func<T,object>> Orderby { get; set; }
        public Expression<Func<T,object>> OrderbyDecs { get; set; }

        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagination { get; set; }


    }
}
