using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.Interfaces.Reports;

namespace Inventory.Application.Reports.Excel
{
    public class ExcelSalesReport : ISalesReportDocument
    {
        public string Generate() => "Generating raw data Excel Sales Report...";
    }
}
