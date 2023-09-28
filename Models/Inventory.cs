using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Products.Models;
namespace Inventory.Models
{
    [Table("Inventory")] // Defines table name
    public class Inventory
    {
        [Key] // Creates Primary Key

        [Required] // Marks the property as required
        public int InventoryId { get; set; }
        
        [Required]
        public int QuantityOnHand { get; set; }
        
        [Required]
        public int ReorderPoint { get; set; }
        
        [MaxLength(100)] // Sets maximum length for supplier String
        public string Supplier { get; set; }

        // link to the Product
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
