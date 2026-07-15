namespace Inventory.Application.DTOs
{
    public class InventoryReportDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalValue { get; set; }
    }
}