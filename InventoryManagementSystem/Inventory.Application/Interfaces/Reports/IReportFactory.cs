using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Application.Interfaces.Reports;

namespace Inventory.Application.Interfaces.Reports
{
    public interface IReportFactory
    {
        IInventoryReportDocument CreateInventoryReport();
        ISalesReportDocument CreateSalesReport();
    }
}
