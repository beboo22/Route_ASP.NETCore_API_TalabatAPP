using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.api.DTOs;
using Talabat.Core.ServiceContract.Payment;

namespace Talabat.api.Controllers
{
    public class PaymentController : BaseController
    {
        public IPaymentIntent _paymentIntent { get; }
        public IMapper _mapper { get; }

        public PaymentController(IPaymentIntent paymentIntent,IMapper mapper)
        {
            _paymentIntent = paymentIntent;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<CustomerBasketDto> CreateOrUpadatePyment(string basketId)
        {
            var item = await _paymentIntent.CreateOrUpdatePaymentIntent(basketId);
            var MapCustomer = _mapper.Map<CustomerBasketDto>(item);

            return MapCustomer;

        }
    }
}
