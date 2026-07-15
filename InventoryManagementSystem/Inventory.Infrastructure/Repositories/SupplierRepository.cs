using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}