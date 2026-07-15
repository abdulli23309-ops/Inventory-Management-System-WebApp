using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;

        public SaleService(ISaleRepository saleRepository, IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            return await _saleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _saleRepository.GetAllAsync();
        }

        public async Task AddSaleAsync(Sale sale)
        {
            // 1. Basic Validation
            if (sale.Details == null || !sale.Details.Any())
            {
                throw new ArgumentException("A sale must contain at least one detail/item.");
            }

            // Calculate the TotalAmount safely using LINQ
            sale.CalculateTotal();

            // 2. Process stock updates and checks
            foreach (var detail in sale.Details)
            {
                var product = await _productRepository.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {detail.ProductId} does not exist.");
                }

                // CRITICAL BUSINESS RULE: Prevent overselling!
                if (product.StockQuantity < detail.Quantity)
                {
                    throw new InvalidOperationException($"Not enough stock for {product.Name}. Available: {product.StockQuantity}, Requested: {detail.Quantity}");
                }

                // Apply Business Logic: Decrease stock
                product.RemoveStock(detail.Quantity);
                await _productRepository.UpdateAsync(product);
            }

            // 3. Save the sale to the database
            await _saleRepository.AddAsync(sale);
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            await _saleRepository.UpdateAsync(sale);
        }

        public async Task DeleteSaleAsync(int id)
        {
            await _saleRepository.DeleteAsync(id);
        }
    }
}