using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Order;
using Talabat.Core.Services.Interfaces;

namespace Talabat.APIs.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO model)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var mappedAddress = _mapper.Map<BuyerAddress>(model.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(buyerEmail, model.BasketId, model.DeliveryMethodId, mappedAddress);

            if (order is null)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));

            return Ok(order);

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetAllOrdersForUserAsync(UserEmail);
            if (orders is null)
                return Ok(new ApiResponse(StatusCodes.Status204NoContent));

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetOrderByIdfoUser(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetUserOrderById(userEmail, id);

            if (order is null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"No Orders Found With id: {id}!"));

            return Ok(order);
        }
    }
}
