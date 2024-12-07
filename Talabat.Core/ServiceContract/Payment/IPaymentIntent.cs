using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.basket;

namespace Talabat.Core.ServiceContract.Payment
{
    public interface IPaymentIntent
    {
        public Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId);
    }
}
