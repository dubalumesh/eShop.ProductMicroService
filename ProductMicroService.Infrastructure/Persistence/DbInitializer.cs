using Microsoft.EntityFrameworkCore;
using ProductMicroService.Core.Entities;
using ProductMicroService.Infrastructure.Context;
namespace ProductMicroService.Infrastructure.Persistence
{
    public class DbInitializer(ProductDbContext productDbContext)
    {
        public async Task InitializeAsync()
        {
            // Intentionally left empty — update when ready to seed the DB
            if (productDbContext == null)
            {
                throw new ArgumentNullException(nameof(productDbContext), "AppDbConext cannot be null when initializing the database.");
            }


            // Ensure the database is migrated
            await productDbContext.Database.MigrateAsync();
            if (productDbContext.Products.Any())
            {
                // Database has already been seeded, so we can exit early
                return;
            }
            var products = new List<Product>
            {
               new Product                {
                   ProductName = "Apple iPhone 15 Pro Max",
                   Category = "Electronics",
                   UnitPrice = 1299.99,
                   QuantityInStock = 50
               },
               new Product
               {
                   ProductName = "Samsung Galaxy S23 Ultra",
                   Category = "Electronics",
                   UnitPrice = 1199.99,
                   QuantityInStock = 40
               },
               new Product
               {
                   ProductName = "Sony WH-1000XM4 Headphones",
                   Category = "Electronics",
                   UnitPrice = 349.99,
                   QuantityInStock = 100
               },
               new Product
               {
                   ProductName="Ergonomic Office Chair", Category="Furniture", UnitPrice=249.00, QuantityInStock=10
               },
               new Product
                {
                     ProductName = "Dell XPS 13 Laptop",
                     Category = "Electronics",
                     UnitPrice = 999.99,
                     QuantityInStock = 30
                },
               new Product
                {
                     ProductName = "Apple MacBook Pro 16",
                     Category = "Electronics",
                     UnitPrice = 2399.99,
                     QuantityInStock = 20
                }
            };
            productDbContext.Products.AddRange(products);
            await productDbContext.SaveChangesAsync();
        }
    }
}
