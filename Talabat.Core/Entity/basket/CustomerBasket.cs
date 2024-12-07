using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity.basket
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
            //BasketItem = new List<BasketItem>();
        }

        public string Id { get; set; }

        public List<BasketItem> BasketItem { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }

    }
}
