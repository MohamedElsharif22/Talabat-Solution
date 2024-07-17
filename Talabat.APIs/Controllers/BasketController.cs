using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Interfaces;

namespace Talabat.APIs.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id.ToString());
            if (basket == null)
                return Ok(new CustomerBasket() { Id = id });
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasket customerBasket)
        {
            var basket = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            if (basket == null)
                return BadRequest(new ApiResponse(400));
            return Ok(basket);
        }

        [HttpDelete]
        public async Task Deletebasket(string Id)
        {
            await _basketRepository.DeleteBasketAsync(Id);
        }
    }
}
