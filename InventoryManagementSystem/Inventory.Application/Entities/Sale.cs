using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Entities
{
    public class Sale : BaseEntity
    {
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; private set; }

        public ICollection<SaleDetail> Details { get; set; } = new List<SaleDetail>();
        public void CalculateTotal()
        {
            if (Details != null && Details.Any())
            {
                TotalAmount = Details.Sum(d => d.Quantity * d.UnitPrice);
            }
        }

        public void AddDetail(SaleDetail detail)
        {
            Details.Add(detail);
            TotalAmount += detail.Quantity * detail.UnitPrice;
        }
    }
}
