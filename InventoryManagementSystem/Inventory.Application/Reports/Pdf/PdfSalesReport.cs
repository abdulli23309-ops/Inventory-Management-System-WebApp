using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.Interfaces.Reports;
namespace Inventory.Application.Reports.Pdf
{
    public class PdfSalesReport : ISalesReportDocument
    {
        public string Generate() => "Generating high-quality PDF Sales Report...";
    }
}
