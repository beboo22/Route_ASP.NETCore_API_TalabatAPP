using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entity.basket;

namespace Talabat.api.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }


        [Required]
        public List<BasketItemDto> BasketItem { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
