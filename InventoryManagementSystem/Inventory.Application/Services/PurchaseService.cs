using Inventory.Application.Entities;
using Inventory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository, IProductRepository productRepository)
        {
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
        }

        public async Task<Purchase?> GetPurchaseByIdAsync(int id)
        {
            return await _purchaseRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesAsync()
        {
            return await _purchaseRepository.GetAllAsync();
        }

        public async Task AddPurchaseAsync(Purchase purchase)
        {
            // 1. Basic Validation
            if (purchase.Details == null || !purchase.Details.Any())
            {
                throw new ArgumentException("A purchase must contain at least one detail/item.");
            }

            // Calculate the TotalAmount safely using LINQ (fixes "Collection was modified" crash)
            purchase.CalculateTotal();

            // 2. Process inventory stock updates
            foreach (var detail in purchase.Details)
            {
                var product = await _productRepository.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {detail.ProductId} does not exist.");
                }

                // Apply Business Logic: Increase product stock
                product.AddStock(detail.Quantity);
                await _productRepository.UpdateAsync(product);
            }

            // 3. Save the purchase to the database (EF Core will automatically insert 
            // the parent Purchase first, grab its ID, and save the Details correctly!)
            await _purchaseRepository.AddAsync(purchase);
        }

        public async Task UpdatePurchaseAsync(Purchase purchase)
        {
            await _purchaseRepository.UpdateAsync(purchase);
        }

        public async Task DeletePurchaseAsync(int id)
        {
            await _purchaseRepository.DeleteAsync(id);
        }
    }
}