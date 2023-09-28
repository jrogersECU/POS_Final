using Microsoft.EntityFrameworkCore;
using Products.Models;



    public class POSDbContext : DbContext
{
    public POSDbContext(DbContextOptions<POSDbContext> options) : base(options)
    {
    }

    // DbSet properties
    public DbSet<Product> Products { get; set; }
    public DbSet<Inventory.Models.Inventory> Inventories { get; set; }
    public DbSet<Transactions.Models.TransactionItem> Transactions { get; set; }
}

