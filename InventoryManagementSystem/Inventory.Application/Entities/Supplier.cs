using Inventory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Entities
{
    public class Supplier : BaseEntity, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Product> Products { get; set; } = new List<Product>(); // Navigation Property: A supplier handles many products
    }
}
