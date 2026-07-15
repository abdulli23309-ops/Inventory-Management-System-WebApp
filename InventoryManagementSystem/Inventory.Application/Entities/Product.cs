using Inventory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Entities
{
    public class Product : BaseEntity, ISoftDeletable //also inherits from BaseEntity so the id and createddate is already present here in the class classic example of Dry principle
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Encapsulation: Only the Product class can directly change the stock
        public int StockQuantity { get; private set; }

        // Foreign Keys
        public int CategoryId { get; set; }
        public Category? Category { get; set; } = null!;//dammit operator (null-forgiving operator)

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;

        // Business behaviors (Methods to safely modify encapsulated data)
        public void AddStock(int quantity)
        {
            if (quantity > 0)
                StockQuantity += quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity > 0 && StockQuantity >= quantity)
                StockQuantity -= quantity;
        }
    }
}
