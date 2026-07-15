// Pattern Role: Client (Interacts only with the Abstract Factory and Abstract Products)
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Interfaces;
using Inventory.Application.Enums;
using Inventory.Application.Interfaces.Reports;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Application.DTOs;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IInventoryReportRepository _reportRepository;
        private readonly IServiceProvider _serviceProvider;

        public ReportsController(IInventoryReportRepository reportRepository, IServiceProvider serviceProvider)
        {
            _reportRepository = reportRepository;
            _serviceProvider = serviceProvider;
        }

        // Unchanged existing valuation endpoint (Dapper based)
        [HttpGet("inventory-valuation")]
        public async Task<IActionResult> GetInventoryValuation()
        {
            var reportList = await _reportRepository.GetFullInventoryValuationAsync();
            var total = reportList.Sum(r => r.TotalValue);
            return Ok(new { totalValuation = total });
        }

        // NEW ENDPOINT: GET /api/reports/document?type=inventory&format=pdf
        [HttpGet("document")]
        public IActionResult GetReportDocument([FromQuery] string type, [FromQuery] string format)
        {
            // 1. Parse & validate the incoming format query parameter
            if (!Enum.TryParse<ReportFormat>(format, true, out var reportFormat))
            {
                return BadRequest("Invalid format. Supported formats are 'excel' or 'pdf'.");
            }

            // 2. Dynamically resolve the corresponding Concrete Factory via .NET Keyed DI
            var factory = _serviceProvider.GetKeyedService<IReportFactory>(reportFormat);
            if (factory == null)
            {
                return StatusCode(500, $"No factory registered for format: {reportFormat}");
            }

            // 3. Use the abstract interface to produce the requested abstract product family
            string result;
            if (string.Equals(type, "inventory", StringComparison.OrdinalIgnoreCase))
            {
                IInventoryReportDocument doc = factory.CreateInventoryReport();
                result = doc.Generate(); // Polymorphic behavior!
            }
            else if (string.Equals(type, "sales", StringComparison.OrdinalIgnoreCase))
            {
                ISalesReportDocument doc = factory.CreateSalesReport();
                result = doc.Generate(); // Polymorphic behavior!
            }
            else
            {
                return BadRequest("Invalid report type. Supported types are 'inventory' or 'sales'.");
            }

            return Ok(result);
        }
    }
}