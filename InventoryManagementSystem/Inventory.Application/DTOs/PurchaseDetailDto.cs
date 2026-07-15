using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs
{
    public class PurchaseDetailDto
    {
        // Purchase line information
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than 0.")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Range(typeof(decimal), "0.01", "999999999.99",
            ErrorMessage = "UnitPrice must be greater than 0.")]
        public decimal UnitPrice { get; set; }
    }
}