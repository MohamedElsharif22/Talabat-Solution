using Talabat.Core.Entities.Order;

namespace Talabat.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethod, BuyerAddress shippingAddress);

        Task<IReadOnlyList<Order>?> GetAllOrdersForUserAsync(string buyerEmail);

        Task<Order?> GetUserOrderById(string buyerEmail, int orderId);


    }
}
