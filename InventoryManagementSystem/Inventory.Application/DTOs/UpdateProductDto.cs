using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs
{
    public class UpdateProductDto
    {
        // Product information
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string SKU { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "999999999.99",
            ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        // Relationships
        [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than 0.")]
        public int CategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "SupplierId must be greater than 0.")]
        public int SupplierId { get; set; }
    }
}