using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification.ProductSpec
{
    public class ProductCriteria
    {
        //string? sort, int? BrandId, int? CategoryId
        public string? sort { get; set; }
        private string? search ;
        public string? Search{ get => search; set => search = value.ToLower(); }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? PadgeSize { get; set; }
        public int? Padgenum { get; set; }




    }
}
