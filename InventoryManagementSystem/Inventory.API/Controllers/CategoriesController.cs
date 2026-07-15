using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Application.Entities;
using Inventory.Application.Interfaces;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _productService;

        // Constructor Injection: Ask the IoC Container for the Service
        public CategoriesController(ICategoryService categoryService)
        {
            _productService = categoryService;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var categories = await _productService.GetAllCategoriesAsync();
            return Ok(categories); // Returns 200 OK status with the categories list
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _productService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found."); // Returns 404 Not Found
            }
            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Category category)
        {
            try
            {
                await _productService.AddCategoryAsync(category);
                // Returns 201 Created status and tells client where to fetch the new category
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message); // Returns 400 Bad Request if validation fails
            }
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("ID mismatch between URL path and body data.");
            }

            await _productService.UpdateCategoryAsync(category);
            return NoContent(); // Returns 204 No Content indicating success with no payload returned
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingCategory = await _productService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            await _productService.DeleteCategoryAsync(id); // Using the base CRUD Repository method via Service
            return NoContent();
        }

        
    }
}

