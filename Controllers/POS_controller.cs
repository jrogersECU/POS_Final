using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Models;
using Inventory.Models;
using Transactions.Models;


namespace POS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly POSDbContext _context; 

    public ProductController(POSDbContext context)
    {
        _context = context;
    }

    // GET All Products
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _context.Products.ToList();
        return Ok(products);
    }

    // GET Specific Product by ID
    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Product_Id == id);

        if (product == null)
        {
            return NotFound(); // Product not found
        }

        return Ok(product);
    }

    [HttpGet("Filter")] // Code for filtering
public IActionResult FilterProducts(string name = null, decimal? minPrice = null, decimal? maxPrice = null)
{
    var filteredProducts = _context.Products.AsQueryable();

    if (!string.IsNullOrWhiteSpace(name))
    {
        filteredProducts = filteredProducts.Where(p => p.Name.Contains(name));
    }

    if (minPrice.HasValue)
    {
        filteredProducts = filteredProducts.Where(p => p.Price >= minPrice);
    }

    if (maxPrice.HasValue)
    {
        filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice);
    }

    return Ok(filteredProducts.ToList());
}


    [HttpGet("ProductWithInventory/{id}")]

public IActionResult GetProductWithInventory(int id)
{
    var productWithInventory = _context.Products
        .Include(p => p.Inventories) // Include the related inventory items
        .FirstOrDefault(p => p.Product_Id == id);

    if (productWithInventory == null)
    {
        return NotFound(); // Product not found
    }

    return Ok(productWithInventory);
}


    // POST Create New Product
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Validation failed
        }

        _context.Products.Add(product);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetProductById), new { id = product.Product_Id }, product);
    }

    // PUT Update Product by ID
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Validation failed
        }

        var existingProduct = _context.Products.FirstOrDefault(p => p.Product_Id == id);

        if (existingProduct == null)
        {
            return NotFound(); // Product not found
        }

        existingProduct.Name = product.Name; 
        existingProduct.Price = product.Price;

        _context.SaveChanges();

        return Ok(existingProduct);
    }

    // DELETE Product by ID
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Product_Id == id);

        if (product == null)
        {
            return NotFound(); // Product not found
        }

        _context.Products.Remove(product);
        _context.SaveChanges();

        return NoContent(); // Success, no content to return
    }

    // Inventory Endpoints

    // GET All Inventory Items
    [HttpGet("Inventory")]
    public IActionResult GetAllInventory()
    {
        var inventory = _context.Inventories.ToList();
        return Ok(inventory);
    }

    // GET Specific Inventory Item by ID
    [HttpGet("Inventory/{id}")]
    public IActionResult GetInventoryById(int id)
    {
        var inventoryItem = _context.Inventories.FirstOrDefault(i => i.InventoryId == id);

        if (inventoryItem == null)
        {
            return NotFound(); // Inventory item not found
        }

        return Ok(inventoryItem);
    }



// POST Create New Inventory Item
[HttpPost("Inventory")]
public IActionResult CreateInventoryItem([FromBody] Inventory.Models.Inventory inventoryItem)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState); // Validation failed
    }

    _context.Inventories.Add(inventoryItem);
    _context.SaveChanges();

    return CreatedAtAction(nameof(GetInventoryById), new { id = inventoryItem.InventoryId }, inventoryItem);
}

// PUT Update Inventory Item by ID
[HttpPut("Inventory/{id}")]
public IActionResult UpdateInventoryItem(int id, [FromBody] Inventory.Models.Inventory inventoryItem)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState); // Validation failed
    }

    var existingInventoryItem = _context.Inventories.FirstOrDefault(i => i.InventoryId == id);

    if (existingInventoryItem == null)
    {
        return NotFound(); // Inventory item not found
    }

    
    existingInventoryItem.QuantityOnHand = inventoryItem.QuantityOnHand;
    existingInventoryItem.ReorderPoint = inventoryItem.ReorderPoint;
    existingInventoryItem.Supplier = inventoryItem.Supplier;

    _context.SaveChanges();

    return Ok(existingInventoryItem);
}

}


    


