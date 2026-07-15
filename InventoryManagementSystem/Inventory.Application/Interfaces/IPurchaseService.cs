using Inventory.Application.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase?> GetPurchaseByIdAsync(int id);
        Task<IEnumerable<Purchase>> GetAllPurchasesAsync();
        Task AddPurchaseAsync(Purchase purchase);
        Task UpdatePurchaseAsync(Purchase purchase);
        Task DeletePurchaseAsync(int id);
    }
}
