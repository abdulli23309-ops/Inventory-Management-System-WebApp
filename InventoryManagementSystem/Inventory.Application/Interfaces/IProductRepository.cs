using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Application.Entities;

namespace Inventory.Application.Interfaces
{
    // Inherits the generic IRepository to get standard CRUD (GetById, Add, etc.)
    // AND allows us to add custom methods only meant for Products.
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    }
}