using Talabat.Core;
using Talabat.Core.Entities.Order;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.Services.Interfaces;
using Talabat.Core.Specifications.Order_Specs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        // Create Order For The User
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethod, BuyerAddress shippingAddress)
        {

            // 1. Get BasketItems
            var basket = await _basketRepository.GetBasketAsync(basketId);

            //Check if any Items Exist
            if (basket?.Items.Count > 0)
            {

                // create list of order Items
                var OrderItems = new List<OrderItem>();

                foreach (var item in basket.Items)
                {
                    var productsOrdered = new ProductItemOrdered(item.Id, item.ProductName, item.PictureUrl);
                    var orderItem = new OrderItem(productsOrdered, item.Price, item.Quantity);

                    OrderItems.Add(orderItem);
                }

                //Intialize order delivery Method to Its value
                var DMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethod);

                var subTotal = OrderItems.Sum(I => I.Price * I.Quentity);

                var order = new Order(buyerEmail, shippingAddress, DMethod, OrderItems, subTotal);
                // Adding Order  Locally
                await _unitOfWork.Repository<Order>().AddAsync(order);

                // save Changes To Database
                var result = await _unitOfWork.CompleteAsync();

                if (result <= 0)
                    return null;

                return order;
            }


            return null;
        }

        // Retrieve All Orders For The User
        public async Task<IReadOnlyList<Order>?> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var Specs = new OrderWithNavigationsSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Specs);

            return orders.ToList();
        }

        // Retrieve Specific Order For The User
        public async Task<Order?> GetUserOrderById(string buyerEmail, int orderId)
        {
            var specs = new OrderWithNavigationsSpecifications(buyerEmail, orderId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecAsync(specs);

            return order;
        }

    }
}
