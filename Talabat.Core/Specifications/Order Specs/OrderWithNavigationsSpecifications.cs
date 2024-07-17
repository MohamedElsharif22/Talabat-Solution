using Talabat.Core.Entities.Order;

namespace Talabat.Core.Specifications.Order_Specs
{
    public class OrderWithNavigationsSpecifications : BaseSpecifications<Order>
    {
        public OrderWithNavigationsSpecifications(string buyerEmail) : base(O => O.BuyerEmail == buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            OrderByDesc = O => O.OrderDate;
        }
        public OrderWithNavigationsSpecifications(string buyerEmail, int OrderId)
                : base(O => O.BuyerEmail == buyerEmail && O.Id == OrderId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            OrderByDesc = O => O.OrderDate;
        }
    }
}
