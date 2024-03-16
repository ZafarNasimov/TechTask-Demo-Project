using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using SharedLib;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _dbContext.Product.ToListAsync();
            return Ok(products);
        }

        [HttpGet("filter")]
        [Route("api/products/filter")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string filter)
        {
            IQueryable<Product> query = _dbContext.Product;

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(p => p.Name.Contains(filter));
            }

            var products = await query.ToListAsync();
            return Ok(products);
        }

        [HttpPost()]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _dbContext.Product.Add(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            try
            {
                var _product = await _dbContext.Product.FirstOrDefaultAsync(p => p.Id == product.Id);
                _product.Name= product.Name;
                _product.Description = product.Description;
                await _dbContext.SaveChangesAsync();
                return Ok("Product Updated!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _dbContext.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Product.Remove(product);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _dbContext.Product.Any(e => e.Id == id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _dbContext.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
