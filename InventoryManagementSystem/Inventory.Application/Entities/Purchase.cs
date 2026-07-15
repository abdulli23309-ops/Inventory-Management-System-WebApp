using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Entities
{
    public class Purchase : BaseEntity
    {
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        // Encapsulation: Prevent random modifications to invoice totals
        public decimal TotalAmount { get; private set; }

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; } = null!;

        public ICollection<PurchaseDetail> Details { get; set; } = new List<PurchaseDetail>();

        public void CalculateTotal()
        {
            if (Details != null && Details.Any())
            {
                TotalAmount = Details.Sum(d => d.Quantity * d.UnitPrice);
            }
        }

        // Domain Logic: Safely append items and calculate totals in one place
        public void AddDetail(PurchaseDetail detail)
        {
            Details.Add(detail);
            TotalAmount += detail.Quantity * detail.UnitPrice;
        }
    }
}
