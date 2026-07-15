using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}