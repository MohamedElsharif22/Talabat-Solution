namespace Talabat.APIs.DTOs
{
    public class AddressDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; } = string.Empty;
    }
}
