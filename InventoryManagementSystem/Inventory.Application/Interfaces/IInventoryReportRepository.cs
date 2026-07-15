using Inventory.Application.DTOs;

namespace Inventory.Application.Interfaces
{
    public interface IInventoryReportRepository
    {
        Task<IEnumerable<InventoryReportDto>> GetFullInventoryValuationAsync();
    }
}