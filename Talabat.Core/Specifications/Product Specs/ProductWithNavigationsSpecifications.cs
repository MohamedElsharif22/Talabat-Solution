using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithNavigationsSpecifications : BaseSpecifications<Product>
    {
        // Prametter Less Constructor For getting All Products
        public ProductWithNavigationsSpecifications(ProductSpecsParams productSpecs)
            : base(
                  P => (string.IsNullOrWhiteSpace(productSpecs.Search) || P.Name.Contains(productSpecs.Search))
                    && (!productSpecs.BrandId.HasValue || P.BrandId == productSpecs.BrandId)
                    && (!productSpecs.CategoryId.HasValue || P.CategoryId == productSpecs.CategoryId)
                  )
        {
            IncludeNavigationalProperties();

            if (!string.IsNullOrWhiteSpace(productSpecs.Sort))
            {
                switch (productSpecs.Sort)
                {
                    case "priceaAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }

            ApplyPagination((productSpecs.PageSize * (productSpecs.PageIndex - 1)), productSpecs.PageSize);
        }

        // Prameterize Constructor to Get Specific Product
        public ProductWithNavigationsSpecifications(int id) : base(P => P.Id == id)
        {
            IncludeNavigationalProperties();
        }

        private void IncludeNavigationalProperties()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
