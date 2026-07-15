using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs
{
    public class CreateSaleDto
    {
        // Sale header
        [Required]
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        // Sale lines
        [Required]
        [MinLength(1, ErrorMessage = "At least one sale detail is required.")]
        public List<SaleDetailDto> Details { get; set; } = new();
    }
}