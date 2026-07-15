using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs
{
    public class UpdatePurchaseDto
    {
        // Purchase header only
        [Required]
        public DateTime PurchaseDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "SupplierId must be greater than 0.")]
        public int SupplierId { get; set; }
    }
}