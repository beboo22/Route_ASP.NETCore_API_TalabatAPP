using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specification.ProductSpec
{
    public class ProductwithSpec : Specification<Product>
    {
        public ProductwithSpec(ProductCriteria spec) 
            : base(P=>
            (string.IsNullOrEmpty(spec.Search)||P.Name.ToLower().Contains(spec.Search))&&
            (!spec.BrandId.HasValue ||P.BrandId == spec.BrandId.Value )&&(!spec.CategoryId.HasValue || P.CategoryId == spec.CategoryId.Value)
            )
        {
            AddIncludes();
            if (!string.IsNullOrEmpty(spec.sort))
            {
                switch (spec.sort)
                {
                    case "nameDecs":
                        AddOrderByDecs(p => p.Name);
                        break;
                    case "price":
                        AddOrderBy(p => p.price);
                        break;
                    case "priceDecs":
                        AddOrderByDecs(p => p.price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                };

            }
            else
            {
                AddOrderBy(p => p.Name);
            }

            if(spec.PadgeSize.HasValue&&spec.Padgenum.HasValue)
            //if(spec.p)
            {
                ApplyPagination(((spec.Padgenum.Value - 1) * spec.PadgeSize.Value), spec.PadgeSize.Value);
            }

        }
        public ProductwithSpec(int id) : base(p => p.ID == id)
        {
            AddIncludes();
        }
        private void AddIncludes()
        {
            includes.Add(p => p.productBrand);
            includes.Add(p => p.productCategory);
        }
    }
}
