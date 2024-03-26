using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Services;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private readonly IMapper _mapper;

        public ProductsController(DatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _databaseService.GetProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByFilter(string filter)
        {
            var products = await _databaseService.GetProductsByFilterAsync(filter);
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }

        [HttpPost("")]
        public async Task<ActionResult<ProductDto>> AddProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var addedProduct = await _databaseService.AddProductAsync(product);
            var addedProductDto = _mapper.Map<ProductDto>(addedProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = addedProductDto.Id }, addedProductDto);
        }

        [HttpPut("")]
        public async Task<ActionResult> UpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var updated = await _databaseService.UpdateProductAsync(product);
            if (updated)
            {
                return Ok("Product Updated!");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var deleted = await _databaseService.DeleteProductAsync(id);
            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var product = await _databaseService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
    }
}