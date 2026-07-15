using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.Interfaces.Reports;

namespace Inventory.Application.Reports.Pdf
{
    public class PdfInventoryReport : IInventoryReportDocument
    {
        public string Generate() => "Generating high-quality PDF Inventory Report...";
    }
}
