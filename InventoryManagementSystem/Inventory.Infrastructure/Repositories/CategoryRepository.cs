using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}