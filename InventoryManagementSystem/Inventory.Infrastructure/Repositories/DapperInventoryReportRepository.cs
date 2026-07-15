using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Application.DTOs;

namespace Inventory.Infrastructure.Repositories
{
    public class DapperInventoryReportRepository : IInventoryReportRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperInventoryReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public async Task<IEnumerable<InventoryReportDto>> GetFullInventoryValuationAsync()
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            string sql = @"
        SELECT 
            p.Id AS ProductId, 
            p.Name AS ProductName, 
            c.Name AS CategoryName, 
            s.Name AS SupplierName, 
            p.StockQuantity AS StockQuantity,  -- FIX: Changed p.Quantity to p.StockQuantity
            p.Price AS UnitPrice, 
            (p.StockQuantity * p.Price) AS TotalValue -- FIX: Changed p.Quantity to p.StockQuantity
        FROM Products p
        INNER JOIN Categories c ON p.CategoryId = c.Id
        INNER JOIN Suppliers s ON p.SupplierId = s.Id
        ORDER BY c.Name, p.Name";

            return await connection.QueryAsync<InventoryReportDto>(sql);
        }
    }
}