using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    // 1. Inherits Repository<Product> -> Gives us CRUD methods for free
    // 2. Implements IProductRepository -> Enforces our custom methods
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        // Passes the DbContext to the base Generic Repository
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Our custom query specific to Products
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            // We can use _dbSet because we marked it as 'protected' in the base class!
            return await _dbSet.Where(p => p.CategoryId == categoryId).ToListAsync();
        }
    }
}