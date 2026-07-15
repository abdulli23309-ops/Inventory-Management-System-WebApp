using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : Controller
    {
        private readonly IPurchaseService _purchaseService;

        // Constructor Injection: Ask the IoC Container for the Service
        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        // GET: api/purchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetAll()
        {
            var purchases = await _purchaseService.GetAllPurchasesAsync();
            return Ok(purchases); // Returns 200 OK status with the purchases list
        }

        // GET: api/purchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetById(int id)
        {
            var purchase = await _purchaseService.GetPurchaseByIdAsync(id);
            if (purchase == null)
            {
                return NotFound($"Purchase with ID {id} not found."); // Returns 404 Not Found
            }
            return Ok(purchase);
        }

        // POST: api/purchases
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Purchase purchase)
        {
            try
            {
                await _purchaseService.AddPurchaseAsync(purchase);
                // Returns 201 Created status and tells client where to fetch the new purchase
                return CreatedAtAction(nameof(GetById), new { id = purchase.Id }, purchase);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message); // Returns 400 Bad Request if validation fails
            }
        }

        // PUT: api/purchases/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Purchase purchase)
        {
            if (id != purchase.Id)
            {
                return BadRequest("ID mismatch between URL path and body data.");
            }

            await _purchaseService.UpdatePurchaseAsync(purchase);
            return NoContent(); // Returns 204 No Content indicating success with no payload returned
        }

        // DELETE: api/purchases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingPurchase = await _purchaseService.GetPurchaseByIdAsync(id);
            if (existingPurchase == null)
            {
                return NotFound($"Purchase with ID {id} not found.");
            }

            await _purchaseService.DeletePurchaseAsync(id); // Using the base CRUD Repository method via Service
            return NoContent();
        }

        
    }
}

