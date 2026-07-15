using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs
{
    public class UpdateSaleDto
    {
        // Sale header only
        [Required]
        public DateTime SaleDate { get; set; }
    }
}