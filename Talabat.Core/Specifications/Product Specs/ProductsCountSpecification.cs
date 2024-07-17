using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductsCountSpecification : BaseSpecifications<Product>
    {
        public ProductsCountSpecification(ProductSpecsParams productSpecs)
            : base(
                      P => (string.IsNullOrWhiteSpace(productSpecs.Search) || P.Name.Contains(productSpecs.Search))
                        && (!productSpecs.BrandId.HasValue || P.BrandId == productSpecs.BrandId)
                        && (!productSpecs.CategoryId.HasValue || P.CategoryId == productSpecs.CategoryId)
                  )
        {

        }
    }
}
