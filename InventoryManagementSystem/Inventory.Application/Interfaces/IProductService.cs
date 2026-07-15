using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Application.Entities;

namespace Inventory.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    }
}