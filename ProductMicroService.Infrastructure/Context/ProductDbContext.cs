
using Microsoft.EntityFrameworkCore;
using ProductMicroService.Core.Entities;

namespace ProductMicroService.Infrastructure.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
