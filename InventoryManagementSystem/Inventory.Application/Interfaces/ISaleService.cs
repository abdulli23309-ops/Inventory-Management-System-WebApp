using Inventory.Application.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface ISaleService
    {
        Task<Sale?> GetSaleByIdAsync(int id);
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task AddSaleAsync(Sale sale);
        Task UpdateSaleAsync(Sale sale);
        Task DeleteSaleAsync(int id);
    }
}
