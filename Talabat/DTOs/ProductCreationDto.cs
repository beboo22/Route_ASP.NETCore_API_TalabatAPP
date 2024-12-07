using System.ComponentModel.DataAnnotations;

namespace Talabat.api.DTOs
{
    public class ProductCreationDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string PictureUrl { get; set; } = null!;

        [Required]
        public decimal price { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}
