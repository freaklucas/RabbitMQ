using ApiRabbitMq.Models;
using ApiRabbitMq.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiRabbitMq.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductsController(IProductService productService)
    {
        _productService = productService; 
    }

    [HttpGet("{id:int}")]
    public IActionResult GetProductById(int id)
    {
        try
        {
            var product = _productService.GetProductById(id);

            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public IActionResult GetProductsList()
    {
        try
        {
            var product = _productService.GetProductList();

            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] Product product)
    {
        if (product == null)
            return BadRequest(new { message = "Produto inválido." });
        
        var addedProduct = _productService.AddProduct(product);
        return CreatedAtAction(nameof(GetProductById), new { id = addedProduct.ProductId }, addedProduct);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        if (product == null || product.ProductId != id)
            return BadRequest(new { message = "Dados do produto inválidos." });

        try
        {
            var updatedProduct = _productService.UpdateProduct(product);
            return Ok(updatedProduct);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
