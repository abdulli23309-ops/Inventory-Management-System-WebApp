using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs
{
    public class UpdateSupplierDto
    {
        // Supplier information
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
    }
}