using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specification.ProductSpec
{
    public class ProductCountSpecfication : Specification<Product>
    {
        public ProductCountSpecfication(ProductCriteria spec)
            : base(P =>
            (!spec.BrandId.HasValue || P.BrandId == spec.BrandId.Value) && (!spec.CategoryId.HasValue || P.CategoryId == spec.CategoryId.Value)
            )
        { }
    }
}
