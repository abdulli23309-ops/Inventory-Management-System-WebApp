using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}