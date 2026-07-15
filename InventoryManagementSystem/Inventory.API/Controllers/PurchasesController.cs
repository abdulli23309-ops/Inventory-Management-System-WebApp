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
    public class PurchasesController : ControllerBase
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
            return Ok(purchases);
        }

        // GET: api/purchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetById(int id)
        {
            var purchase = await _purchaseService.GetPurchaseByIdAsync(id);

            if (purchase == null)
            {
                return NotFound($"Purchase with ID {id} not found.");
            }

            return Ok(purchase);
        }

        // POST: api/purchases
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePurchaseDto dto)
        {
            try
            {
                var purchase = new Purchase
                {
                    PurchaseDate = dto.PurchaseDate,
                    SupplierId = dto.SupplierId
                };

                foreach (var item in dto.Details)
                {
                    purchase.AddDetail(new PurchaseDetail
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    });
                }

                await _purchaseService.AddPurchaseAsync(purchase);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = purchase.Id },
                    purchase);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/purchases/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdatePurchaseDto dto)
        {
            var purchase = await _purchaseService.GetPurchaseByIdAsync(id);

            if (purchase == null)
            {
                return NotFound($"Purchase with ID {id} not found.");
            }

            purchase.PurchaseDate = dto.PurchaseDate;
            purchase.SupplierId = dto.SupplierId;

            await _purchaseService.UpdatePurchaseAsync(purchase);

            return NoContent();
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

            await _purchaseService.DeletePurchaseAsync(id);

            return NoContent();
        }
    }
}