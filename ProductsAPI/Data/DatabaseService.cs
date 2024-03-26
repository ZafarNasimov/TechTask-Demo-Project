using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Services
{
    public class DatabaseService
    {
        private readonly AppDbContext _dbContext;

        public DatabaseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByFilterAsync(string filter)
        {
            IQueryable<Product> query = _dbContext.Product;

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(p => p.Name.Contains(filter));
            }

            return await query.ToListAsync();
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            _dbContext.Product.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var existingProduct = await _dbContext.Product.FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _dbContext.Product.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _dbContext.Product.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _dbContext.Product.FindAsync(id);
        }

        public bool ProductExists(Guid id)
        {
            return _dbContext.Product.Any(e => e.Id == id);
        }
    }
}