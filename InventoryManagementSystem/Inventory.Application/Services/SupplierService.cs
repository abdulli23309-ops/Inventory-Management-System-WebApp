using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Application.Entities;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        // Dependency Injection: Ask for the Supplier Repository
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            return await _supplierRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _supplierRepository.GetAllAsync();
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            // Business Logic & Validation
            if (string.IsNullOrWhiteSpace(supplier.Name))
            {
                throw new ArgumentException("Supplier name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(supplier.Phone))
            {
                throw new ArgumentException("Supplier phone number cannot be empty.");
            }

            await _supplierRepository.AddAsync(supplier);
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            // Validation for Updates
            if (string.IsNullOrWhiteSpace(supplier.Name))
            {
                throw new ArgumentException("Supplier name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(supplier.Phone))
            {
                throw new ArgumentException("Supplier phone number cannot be empty.");
            }

            await _supplierRepository.UpdateAsync(supplier);
        }

        public async Task DeleteSupplierAsync(int id)
        {
            await _supplierRepository.DeleteAsync(id);
        }
    }
}