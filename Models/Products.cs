using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Models
{
    public class Product
    {
        [Key] // Primary Key

        public int ProductId { get; set; }
        
        [Required] // Marks property as required
        [MaxLength(100)]// Sets max length constraint
        [Column("ProductName")] // Specifies the column's name
        
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        // additional properties?

        // other methods?

         public ICollection<Inventory.Models.Inventory> Inventories { get; set; }

         public ICollection<Transactions.Models.TransactionItem> TransactionItems { get; set; }

    }
}
