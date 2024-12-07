using System.ComponentModel.DataAnnotations;

namespace Talabat.api.DTOs
{
    public class BasketItemDto
    {

        [Required]
        public int Id { get; set; }

        [Required]
        
        public string ProductName { get; set; }

        [Required]
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage = "Price Must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        [Range(0,int.MaxValue,ErrorMessage = "Quantity must be zero or positive number")]
        public int Quantity { get; set; }

    }
}
