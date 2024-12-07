using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core;
using Talabat.Core.Entity.basket;
using Talabat.Core.Entity.Order_Aggregrate;
using Talabat.Core.Repostiries_contract;
using Talabat.Core.ServiceContract.Payment;
using Product = Talabat.Core.Entity.Product;

namespace Talabat.Service
{
    public class PaymentIntent : IPaymentIntent
    {

        public IConfiguration _Config { get; }
        public IBasketRepository _BasketRepo { get; }
        public IUnitOfWork _UnitOfWork { get; }

        public PaymentIntent(IConfiguration configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _Config = configuration;
            _BasketRepo = basketRepository;
            _UnitOfWork = unitOfWork;
        }


        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
        {
            //StripeConfiguration.
            StripeConfiguration.ApiKey = _Config["Payment:SecretKey"];

            //fetch Basket
            var Basket = await _BasketRepo.GetBasketAsync(BasketId);

            //count Shipping Price in deliveryMethod
            var ShippingPrice = 0M;
            if (Basket.DeliveryMethodId.HasValue)
            {
                var delivery = await _UnitOfWork.Repo<DeliveryMethod>().GetByID(Basket.DeliveryMethodId.Value);
                ShippingPrice = delivery.Cost;
            }
            //count SubtotalPrice
            if (Basket.BasketItem.Count > 0)
            {
                foreach (var item in Basket.BasketItem)
                {
                    var Prod = await _UnitOfWork.Repo<Product>().GetByID(item.Id);
                    if (item.Price != Prod.price)
                        item.Price = Prod.price;
                }
            }

            var suptotalPrice = Basket.BasketItem?.Sum(x => x.Price * x.Quantity);

            //create or Update Payment 

            var Service = new PaymentIntentService();
            Stripe.PaymentIntent pay;
            if (Basket.PaymentIntentId is null)//Create
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)suptotalPrice * 100 + (long)ShippingPrice * 100,
                    Currency = "Usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                pay = await Service.CreateAsync(option);
                Basket.PaymentIntentId = pay.Id;
                Basket.ClientSecret = pay.ClientSecret;
            }
            else //Update
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)suptotalPrice * 100 + (long)ShippingPrice * 100,
                };
                pay = await Service.UpdateAsync(Basket.Id,option);

                Basket.PaymentIntentId=pay.Id;
                Basket.ClientSecret = pay.ClientSecret;
            }

            Basket = await _BasketRepo.UpdateBasketAsync(Basket);

            return Basket;




        }
        
    }
}
