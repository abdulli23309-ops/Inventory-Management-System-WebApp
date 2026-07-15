// Pattern Role: Concrete Factory (Produces Excel-formatted family of products)
using Inventory.Application.Interfaces.Reports;
using Inventory.Application.Reports.Excel;

namespace Inventory.Application.Factories
{
    public class ExcelReportFactory : IReportFactory
    {
        public IInventoryReportDocument CreateInventoryReport() => new ExcelInventoryReport();
        public ISalesReportDocument CreateSalesReport() => new ExcelSalesReport();
    }
}