using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data
{
    public static class StoreDbContextSeed
    {
        //Seed Data
        public static async Task SeedAsync(StoreDbContext _context)
        {

            // brands Seeding
            if (_context?.ProductBrands.Count() == 0)
            {
                // Read All data From Seeding file
                var brandsData = File.ReadAllText(@"../Talabat.Repository/Data/DataSeed/brands.json");

                // Convert "Brands Data" Form String into List Of "ProductBrands"
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        _context.Set<ProductBrand>().Add(brand);
                    }
                }
            }

            //=====================================

            // Categories Seeding
            if (_context?.ProductCategories.Count() == 0)
            {
                // Read All data From Seeding file
                var CategoriesData = File.ReadAllText(@"../Talabat.Repository/Data/DataSeed/categories.json");

                // Convert "categories Data" Form String into List Of "ProductCategories"
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);
                if (categories?.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        _context.Set<ProductCategory>().Add(category);
                    }
                }
            }
            //======================================

            //Product Seeding
            if (_context?.Products.Count() == 0)
            {
                // Read All data From Seeding file
                var productData = File.ReadAllText(@"../Talabat.Repository/Data/DataSeed/products.json");

                // Convert "Products Data" Form String into List Of "Products"
                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        _context.Set<Product>().Add(product);
                    }
                }
                await _context.SaveChangesAsync();
            }

            //======================================

            //Delivery Seeding
            if (_context?.DeliveryMethods.Count() == 0)
            {
                // Read All data From Seeding file
                var DeliveryData = File.ReadAllText(@"../Talabat.Repository/Data/DataSeed/delivery.json");

                // Convert "DeliveryMethods Data" Form String into List Of "DeliveryMethods"
                var DMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

                if (DMethods?.Count > 0)
                {
                    foreach (var DMethod in DMethods)
                    {
                        _context.Set<DeliveryMethod>().Add(DMethod);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
