using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Products.Models;
using Inventory.Models;


namespace Transactions.Models
{
    [Table("Transactions")] // Defines table name 
    public class Transaction
    {
        [Key] // Defines the primary key
        public int TransactionId { get; set; }
        
        [Required] // Marks the property as required
        public DateTime TransactionDate { get; set; }
        
        [Required]
        [MaxLength(50)] // Sets max string length for payment method
        public string PaymentMethod { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Defines the column data type
        public decimal TotalAmount { get; set; }

        
        public List<TransactionItem> Items { get; set; }
    }

    [Table("TransactionItems")] // Defines table
    public class TransactionItem
    {
        [Key] // Defines primary key
        public int TransactionItemId { get; set; }
       
       [Required] // Marks property as required
       public int ProductId { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Defines the column data type
        public decimal UnitPrice { get; set; }

        // link to the Product
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
