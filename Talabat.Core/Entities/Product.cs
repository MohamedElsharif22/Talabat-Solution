namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public virtual ProductBrand Brand { get; set; }
        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}
