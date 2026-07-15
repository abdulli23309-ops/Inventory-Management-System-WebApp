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
    public class SalesController : ControllerBase
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
            return Ok(sales);
        }

        // GET: api/sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);

            if (sale == null)
            {
                return NotFound($"Sale with ID {id} not found.");
            }

            return Ok(sale);
        }

        // POST: api/sales
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSaleDto dto)
        {
            try
            {
                var sale = new Sale
                {
                    SaleDate = dto.SaleDate
                };

                foreach (var item in dto.Details)
                {
                    sale.AddDetail(new SaleDetail
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    });
                }

                await _saleService.AddSaleAsync(sale);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = sale.Id },
                    sale);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/sales/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateSaleDto dto)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);

            if (sale == null)
            {
                return NotFound($"Sale with ID {id} not found.");
            }

            // Update only the editable header field.
            // Details and TotalAmount remain unchanged.
            sale.SaleDate = dto.SaleDate;

            await _saleService.UpdateSaleAsync(sale);

            return NoContent();
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

            await _saleService.DeleteSaleAsync(id);

            return NoContent();
        }
    }
}