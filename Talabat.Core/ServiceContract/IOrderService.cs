using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Order_Aggregrate;

namespace Talabat.Core.ServiceContract
{
    public interface IOrderService
    {

        public Task<Order> CreateOrderAsync(string Buyer, string basketId, int deliveryMethod, Address address);

        public Task<IReadOnlyList<Order>> GetOderForUserAsync(string BuyerEmail);
        public Task<Order> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail);

    }
}
