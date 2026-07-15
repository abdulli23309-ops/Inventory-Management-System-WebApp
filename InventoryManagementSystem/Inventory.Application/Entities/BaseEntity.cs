using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Entities
{
    public abstract class BaseEntity// Inheritance used here cause the id and the CreatedDate will be inherited in their child classes 
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
