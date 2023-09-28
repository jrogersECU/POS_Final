using System.Collections.Generic;
using Products.Models;

public static class ProductSeedData
{
    public static readonly List<Product> Products = new List<Product>
    {
        new Product { Name = "Hotdog", Price = 10.99m },
        new Product { Name = "Hamburger", Price = 15.99m },
        // Add more products later
    };
}
