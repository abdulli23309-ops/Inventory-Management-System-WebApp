using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Application.Entities;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        // Constructor Injection
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            // Business Logic & Validation
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            if (product.Price <= 0)
            {
                throw new ArgumentException("Product price must be greater than zero.");
            }

            // Note: product.StockQuantity automatically defaults to 0 on creation,
            // so we don't need to assign it here, avoiding the private set compiler error!

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            // Validation for Updates
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            if (product.Price <= 0)
            {
                throw new ArgumentException("Product price must be greater than zero.");
            }

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepository.GetProductsByCategoryAsync(categoryId);
        }
    }
}