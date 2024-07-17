namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductSpecsParams
    {

        private const int MaxPageSize = 5;
        private int pageSize = MaxPageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string? Search { get; set; }

    }
}
