using System.Collections.Generic;
using Products.Models;

public static class ProductSeedData
{
    public static readonly List<Product> Products = new List<Product>
    {
        new Product { Name = "Hotdog", Price = 10.99m },
        new Product { Name = "Hamburger", Price = 15.99m },
        new Product { Name = "Arroz con Pollo", Price = 12.99m },
        new Product { Name = "Bulgogi Tacos", Price = 14.99m },
        new Product { Name = "Ceviche", Price = 13.99m },
        new Product { Name = "Chimichanga", Price = 11.99m },
    };
}
