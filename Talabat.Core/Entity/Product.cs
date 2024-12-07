using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
                     
        public string PictureUrl { get; set; } = null!;
                        
        public decimal price { get; set; }

        public int? CategoryId { get; set; }
        public ProductCategory productCategory { get; set; }


        public int? BrandId { get; set; }
        public ProductBrand productBrand { get; set; }
    }
}
