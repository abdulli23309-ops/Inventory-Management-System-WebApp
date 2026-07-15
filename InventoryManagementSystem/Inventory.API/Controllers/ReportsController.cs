using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Interfaces;

namespace Inventory.API.Controllers // Adjust namespace if yours is different
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IInventoryReportRepository _reportRepository;

        public ReportsController(IInventoryReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        // THIS is the exact route the frontend is looking for: /api/reports/inventory-valuation
        [HttpGet("inventory-valuation")]
        public async Task<IActionResult> GetInventoryValuation()
        {
            // 1. Get the list from your Dapper repository
            var reportList = await _reportRepository.GetFullInventoryValuationAsync();

            // 2. Sum up the 'TotalValue' of all products so the frontend gets the exact number it needs
            var total = reportList.Sum(r => r.TotalValue);

            // 3. Return it in the exact JSON format the React component expects: { totalValuation: 1234.56 }
            return Ok(new { totalValuation = total });
        }
    }
}