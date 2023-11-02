using Microsoft.EntityFrameworkCore;
using Products.Models;
using Transactions.Models;
using Inventory.Models;
using UserData;



    public class POSDbContext : DbContext
{
    public POSDbContext(DbContextOptions<POSDbContext> options) : base(options)
    {
    }

    // DbSet properties
    public DbSet<Product> Products { get; set; }
    public DbSet<Inventory.Models.Inventory> Inventories { get; set; }
    public DbSet<Transactions.Models.TransactionItem> Transactions { get; set; }
    public DbSet<User> Users { get; set; }

     // Method for seeding tables
     public void SeedData()
    {
        if (!Products.Any())
        {
        
            var product1 = new Product { Name = "Hot Dog" };
            var product2 = new Product { Name = "Hamburger" };

            Products.AddRange(product1, product2);
            SaveChanges();
        }

        if (!Inventories.Any())
        {
        var product1 = Products.Single(p => p.Name == "Hot Dog");
        var product2 = Products.Single(p => p.Name == "Hamburger");
        
        var inventory1 = new Inventory.Models.Inventory { ProductId = product1.ProductId, QuantityOnHand = 100, ReorderPoint = 20, Supplier = "Supplier A" };
        var inventory2 = new Inventory.Models.Inventory { ProductId = product2.ProductId, QuantityOnHand = 75, ReorderPoint = 15, Supplier = "Supplier B" };


        Inventories.AddRange(inventory1, inventory2);
        SaveChanges();
        }

 
        var transactionItem = new TransactionItem
        {
            ProductId = 1, 
            Quantity = 5,
            UnitPrice = 10.00m 
        };

        Set<TransactionItem>().Add(transactionItem);
        SaveChanges();
    }


     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        base.OnModelCreating(modelBuilder);
        // Custom index for the name of product entities
        modelBuilder.Entity<Product>()
    
        .HasIndex(p => p.Name)
        .IsUnique();

        modelBuilder.Entity<User>().ToTable("Users");
    }  
}

