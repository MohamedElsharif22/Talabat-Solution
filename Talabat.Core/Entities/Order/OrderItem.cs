namespace Talabat.Core.Entities.Order
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {

        }
        public OrderItem(ProductItemOrdered productItem, decimal price, int quentity)
        {
            ProductItem = productItem;
            Price = price;
            Quentity = quentity;
        }

        public ProductItemOrdered ProductItem { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
    }
}
