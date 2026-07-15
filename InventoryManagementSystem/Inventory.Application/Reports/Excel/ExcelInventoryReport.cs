using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.Interfaces.Reports;

namespace Inventory.Application.Reports.Excel
{
    public class ExcelInventoryReport : IInventoryReportDocument
    {
        public string Generate() => "Generating raw data Excel Inventory Report...";
    }
}