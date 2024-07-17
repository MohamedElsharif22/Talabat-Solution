using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class OrderDTO
    {
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; }
    }
}
