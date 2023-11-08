using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Products.Models;
using Inventory.Models;
using Transactions.Models;
using LoginModel.Models;


namespace POS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly POSDbContext _context; 
    private readonly IConfiguration _configuration; 

    public ProductController(POSDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] Login model)
    {
        var tokenService = new TokenService(_configuration, _context); // Pass your configuration to the TokenService
        var token = tokenService.GenerateToken(model); // Generate the JWT token

        if (token == null)
        {
            return Unauthorized();
        }
        
        return Ok(new { token });
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpGet("admin-endpoint")]
    public IActionResult AdminEndpoint()
    {
        // Check if the current user is an admin
        if (User.IsInRole("Admin"))
        {
        // Admin-specific logic here
            return Ok("Welcome, Admin!");
        }
        else
        {
            return Forbid(); // Return a 403 Forbidden response if the user is not authorized.
        }

    }

    [Authorize(Policy = "UserPolicy")]
    [HttpGet("user-endpoint")]
    public IActionResult UserEndpoint()
    {
        // Check if the current user is a user
        if (User.IsInRole("User"))
        {
        // User-specific logic here
            return Ok("Welcome, User!");
        }
        else
        {
            return Forbid(); // Return a 403 Forbidden response if the user is not authorized.
        }
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
        var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

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
        .FirstOrDefault(p => p.ProductId == id);

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

        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
    }

    // PUT Update Product by ID
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Validation failed
        }

        var existingProduct = _context.Products.FirstOrDefault(p => p.ProductId == id);

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
        var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

        if (product == null)
        {
            return NotFound(); // Product not found
        }

        _context.Products.Remove(product);
        _context.SaveChanges();

        return NoContent(); // Success, no content to return
    }

    // DELETE Inventory Item by ID
    [HttpDelete("Inventory/{id}")]
    public IActionResult DeleteInventoryItem(int id)
    {
        var inventoryItem = _context.Inventories.FirstOrDefault(i => i.InventoryId == id);

        if (inventoryItem == null)
        {
            return NotFound(); // Inventory item not found
        }

        _context.Inventories.Remove(inventoryItem);
        _context.SaveChanges();

        return NoContent(); // Success
}

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

// I am adding this comment to test migration rollback. This accomplishes nothing.


    


