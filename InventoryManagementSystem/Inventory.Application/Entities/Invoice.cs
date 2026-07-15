using System;
using Inventory.Application.Enums;

namespace Inventory.Application.Entities
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public InvoiceType Type { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}