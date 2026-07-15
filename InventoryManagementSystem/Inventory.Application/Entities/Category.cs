using Inventory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Entities
{
    public class Category : BaseEntity, ISoftDeletable// inheriting from the baseentity class to get the id and created date properties is already present here in the class classic example of Dry principle
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Product> Products { get; set; } = new List<Product>();// Navigation Property: One Category can have many Products
    }
}
