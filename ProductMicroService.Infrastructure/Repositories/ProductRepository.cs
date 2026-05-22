using Microsoft.EntityFrameworkCore;
using ProductMicroService.Core.Entities;
using ProductMicroService.Core.IRepository;

namespace ProductMicroService.Infrastructure.Repositories
{
    internal class ProductRepository : IProductsRepository
    {
        private readonly Context.ProductDbContext _context;

        public ProductRepository(Context.ProductDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// add new product to the database and return the added product
        /// </summary>
        /// <param name="product">new product</param>
        /// <returns>The added product</returns>
        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        /// <summary>
        /// Deletes a product by its ID. Returns true if the product was found and deleted, false otherwise.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>True if the product was found and deleted, false otherwise.</returns>
        public async Task<bool> DeleteProductAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Product> GetProductByIdAsync(int id)
        {
            Product? product = _context.Products.Find(id);
            if (product == null)
            {
                return Task.FromResult<Product>(null);
            }
            return Task.FromResult(product);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<Product> GetProductByNameAsync(string name)
        {
            Product? product = _context.Products.FirstOrDefault(p => p.ProductName == name);
            if (product == null)
            {
                return Task.FromResult<Product>(null);
            }
            return Task.FromResult(product);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            IEnumerable<Product> products = _context.Products.ToList();
            return Task.FromResult(products);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        public async Task<Product> UpdateProductAsync(Product product)
        {
            Product? existingProduct = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct == null)
            {
                return null;
            }
            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}
