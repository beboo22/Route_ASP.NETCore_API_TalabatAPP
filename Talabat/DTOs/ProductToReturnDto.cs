using Talabat.Core.Entity;

namespace Talabat.api.DTOs
{
    public class ProductToReturnDto 
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public decimal price { get; set; }

        public int CategoryId { get; set; }
        public string productCategory { get; set; }


        public int BrandId { get; set; }
        public string productBrand { get; set; }
    }
}
