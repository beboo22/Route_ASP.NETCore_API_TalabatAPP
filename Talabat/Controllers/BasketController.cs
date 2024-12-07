using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.api.Attributes;
using Talabat.api.DTOs;
using Talabat.api.Errors;
using Talabat.Core.Entity.basket;
using Talabat.Core.Repostiries_contract;

namespace Talabat.api.Controllers
{
    public class BasketController : BaseController
    {
        private IBasketRepository _BR;
        private readonly IMapper _map;

        public BasketController(IBasketRepository BR,IMapper map)
        {
            _BR = BR;
            _map = map;
        }
        [Cache(30)]
        [HttpGet("{id}")]

        public async Task<ActionResult<CustomerBasket?>> GetBasket(string id)
        {
            var item = await _BR.GetBasketAsync(id);


            return Ok(item ?? new CustomerBasket(id));

        }



        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateOrCreated(CustomerBasketDto item)
        {
            var map = _map.Map<CustomerBasketDto,CustomerBasket>(item);

            var UpdatedOrCreatedItem = await _BR.UpdateBasketAsync(map);

            return UpdatedOrCreatedItem is null? BadRequest(new ApiResponse(400)) : Ok(UpdatedOrCreatedItem);


        }


        [HttpDelete]
        public async Task Deleteitem(string id)
        {
            await _BR.DeleteBasketAsync(id);
        }






    }
}
