using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Application.Entities;
using Inventory.Application.Interfaces;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Route will be: api/products
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // Constructor Injection: Ask the IoC Container for the Service
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products); // Returns 200 OK status with the products list
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found."); // Returns 404 Not Found
            }
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Product product)
        {
            try
            {
                await _productService.AddProductAsync(product);
                // Returns 201 Created status and tells client where to fetch the new product
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message); // Returns 400 Bad Request if validation fails
            }
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("ID mismatch between URL path and body data.");
            }

            await _productService.UpdateProductAsync(product);
            return NoContent(); // Returns 204 No Content indicating success with no payload returned
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            await _productService.DeleteProductAsync(id); // Using the base CRUD Repository method via Service
            return NoContent();
        }

        // GET: api/products/category/3
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }
    }
}