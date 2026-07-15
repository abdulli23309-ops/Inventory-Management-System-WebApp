// Pattern Role: Concrete Factory (Produces PDF-formatted family of products)
using Inventory.Application.Interfaces.Reports;
using Inventory.Application.Reports.Pdf;

namespace Inventory.Application.Factories
{
    public class PdfReportFactory : IReportFactory
    {
        public IInventoryReportDocument CreateInventoryReport() => new PdfInventoryReport();
        public ISalesReportDocument CreateSalesReport() => new PdfSalesReport();
    }
}