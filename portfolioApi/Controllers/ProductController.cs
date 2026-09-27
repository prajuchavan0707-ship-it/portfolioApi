using Microsoft.AspNetCore.Mvc;
using portfolioApi.Model;
using portfolioApi.Repository;

namespace portfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    // Constructor Injection
    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    // 1. GET ALL PRODUCTS
    // GET: api/Product
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _productRepository.GetAll();
        return Ok(result);
    }

    // 2. GET PRODUCT BY ID
    // GET: api/Product/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }
        return Ok(product);
    }

    // 3. POST NEW PRODUCT
    // POST: api/Product
    [HttpPost]
    public IActionResult Post([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest(new { message = "Invalid product data." });
        }

        var createdProduct = _productRepository.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    // 4. PUT (UPDATE) PRODUCT BY ID
    // PUT: api/Product/1
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Product updatedProduct)
    {
        if (updatedProduct == null)
        {
            return BadRequest(new { message = "Invalid product data." });
        }

        var product = _productRepository.Update(id, updatedProduct);
        if (product == null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }

        return Ok(new { message = "Product updated successfully.", product });
    }

    // 5. DELETE PRODUCT BY ID
    // DELETE: api/Product/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _productRepository.Delete(id);
        if (!success)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }

        return Ok(new { message = $"Product with ID {id} deleted successfully." });
    }
}