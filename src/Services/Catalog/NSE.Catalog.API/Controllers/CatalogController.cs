using Microsoft.AspNetCore.Mvc;
using NSE.Catalog.API.Models;

namespace NSE.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("products")]
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("products/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpPost("products")]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("products/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct is null) return NotFound();

            await _productRepository.UpdateAsync(product);
            return NoContent();
        }
    }
}
