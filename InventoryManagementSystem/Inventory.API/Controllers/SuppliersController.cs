using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        // Constructor Injection: Ask the IoC Container for the Service
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET: api/suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAll()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers); // Returns 200 OK status with the suppliers  list
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetById(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound($"Supplier with ID {id} not found."); // Returns 404 Not Found
            }
            return Ok(supplier);
        }

        // POST: api/suppliers
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Supplier supplier)
        {
            try
            {
                await _supplierService.AddSupplierAsync(supplier);
                // Returns 201 Created status and tells client where to fetch the new supplier
                return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message); // Returns 400 Bad Request if validation fails
            }
        }

        // PUT: api/suppliers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest("ID mismatch between URL path and body data.");
            }

            await _supplierService.UpdateSupplierAsync(supplier);
            return NoContent(); // Returns 204 No Content indicating success with no payload returned
        }

        // DELETE: api/suppliers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingSupplier = await _supplierService.GetSupplierByIdAsync(id);
            if (existingSupplier == null)
            {
                return NotFound($"Supplier with ID {id} not found.");
            }

            await _supplierService.DeleteSupplierAsync(id); // Using the base CRUD Repository method via Service
            return NoContent();
        }

        
    }

}
