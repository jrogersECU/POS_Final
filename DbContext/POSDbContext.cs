using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Products.Models;
using Transactions.Models;
using Inventory.Models;
using UserData;
using PasswordHasher;



using Microsoft.Extensions.Logging.Abstractions;




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
        InsertInitialProducts();
        
        if (!Transactions.Any())
        {
        var product1 = Products.Single(p => p.Name == "Hot Dog");
        var product2 = Products.Single(p => p.Name == "Hamburger");
        
        var transactionItem1 = new TransactionItem { ProductId = 3, Quantity = 5, UnitPrice = 5.00m };
        var transactionItem2 = new TransactionItem { ProductId = 4, Quantity = 5, UnitPrice = 7.00m };


        Transactions.AddRange(transactionItem1, transactionItem2);
        SaveChanges();
        }

    }

     public void InsertInitialProducts()
    {
        if (!Products.Any(p => p.ProductId == 3))
        {
            var product1 = new Product { ProductId = 3, Name = "Hot Dog", Price = 5.00m };
            Products.Add(product1);
        }

        if (!Products.Any(p => p.ProductId == 4))
        {
            var product2 = new Product { ProductId = 4, Name = "Hamburger", Price = 7.00m };
            Products.Add(product2);
        }

        SaveChanges();

        if (!Users.Any())
    {
        
        var user1 = new User
        {
            Username = "user1",
            Email = "user1@example.com",
            PasswordSalt = SaltGenerator.GenerateRandomSalt(),
            PasswordHash = PasswordHasher1.HashPassword("password123"),
            Role = "User", 
            // Other user properties
        };

        var user2 = new User
        {
            Username = "admin1",
            Email = "admin1@example.com",
            PasswordSalt = SaltGenerator.GenerateRandomSalt(),
            PasswordHash = PasswordHasher1.HashPassword("password321"),
            Role = "Admin",
        };

        Users.AddRange(user1, user2);
        SaveChanges();
    }

    
    }


     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        // Custom index for the name of product entities
        modelBuilder.Entity<Product>()
    
        .ToTable("Products")
        .HasKey(p => p.ProductId);

        modelBuilder.Entity<Product>()
        .Property(p => p.ProductId)
        .HasColumnName("ProductId");
        
        modelBuilder.Entity<Product>()
        .HasIndex(p => p.Name)
        .IsUnique();

        modelBuilder.Entity<Inventory.Models.Inventory>()
        .HasOne(i => i.Product)
        .WithMany(p => p.Inventories)
        .HasForeignKey(i => i.ProductId);

        modelBuilder.Entity<User>().ToTable("Users");
    }  
}

