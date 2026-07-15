using Inventory.Application.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<Supplier?> GetSupplierByIdAsync(int id);
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task AddSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(int id);
    }
}
