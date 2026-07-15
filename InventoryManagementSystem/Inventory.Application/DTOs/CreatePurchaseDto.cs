using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs
{
    public class CreatePurchaseDto
    {
        // Purchase header
        [Required]
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        [Range(1, int.MaxValue, ErrorMessage = "SupplierId must be greater than 0.")]
        public int SupplierId { get; set; }

        // Purchase lines
        [Required]
        [MinLength(1, ErrorMessage = "At least one purchase detail is required.")]
        public List<PurchaseDetailDto> Details { get; set; } = new();
    }
}