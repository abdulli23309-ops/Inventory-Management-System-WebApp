using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        private readonly ISaleService _saleService;

        // Constructor Injection: Ask the IoC Container for the Service
        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // GET: api/sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetAll()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(sales); // Returns 200 OK status with the sales list
        }

        // GET: api/sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound($"Sale with ID {id} not found."); // Returns 404 Not Found
            }
            return Ok(sale);
        }

        // POST: api/sales
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Sale sale)
        {
            try
            {
                await _saleService.AddSaleAsync(sale);
                // Returns 201 Created status and tells client where to fetch the new sale
                return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message); // Returns 400 Bad Request if validation fails
            }
        }

        // PUT: api/sales/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Sale sale)
        {
            if (id != sale.Id)
            {
                return BadRequest("ID mismatch between URL path and body data.");
            }

            await _saleService.UpdateSaleAsync(sale);
            return NoContent(); // Returns 204 No Content indicating success with no payload returned
        }

        // DELETE: api/sales/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingSale = await _saleService.GetSaleByIdAsync(id);
            if (existingSale == null)
            {
                return NotFound($"Sale with ID {id} not found.");
            }

            await _saleService.DeleteSaleAsync(id); // Using the base CRUD Repository method via Service
            return NoContent();
        }

        
    }
}

