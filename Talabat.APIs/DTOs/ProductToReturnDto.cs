namespace Talabat.APIs.DTOs
{
    // Dto => Data Transfer object
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public virtual string Brand { get; set; }
        public int CategoryId { get; set; }
        public virtual string Category { get; set; }
    }
}
