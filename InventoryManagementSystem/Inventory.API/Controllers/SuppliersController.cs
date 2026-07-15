using Inventory.Application.DTOs;
using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
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
            return Ok(suppliers);
        }

        // GET: api/suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetById(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound($"Supplier with ID {id} not found.");
            }

            return Ok(supplier);
        }

        // POST: api/suppliers
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSupplierDto dto)
        {
            try
            {
                var supplier = new Supplier
                {
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Email = dto.Email
                };

                await _supplierService.AddSupplierAsync(supplier);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = supplier.Id },
                    supplier);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/suppliers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateSupplierDto dto)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound($"Supplier with ID {id} not found.");
            }

            supplier.Name = dto.Name;
            supplier.Phone = dto.Phone;
            supplier.Email = dto.Email;

            await _supplierService.UpdateSupplierAsync(supplier);

            return NoContent();
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

            await _supplierService.DeleteSupplierAsync(id);

            return NoContent();
        }
    }
}