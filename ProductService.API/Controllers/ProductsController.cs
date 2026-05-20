using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProductService.Application.DTOs;
using ProductService.Application.Services;

namespace ProductService.API.Controllers;
 
//[Authorize]
[ApiController]
[EnableRateLimiting("fixed")] 
[Route("api/v{version:apiVersion}/[controller]")] 
public class ProductsController(IProductService service) : ControllerBase
{
    private readonly IProductService _service = service;

    // =============================
    // CREATE
    // =============================
    [HttpPost]
    //[Authorize(Roles = "Admin,Manager")] 
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    // =============================
    // GET BY ID
    // =============================
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager,Viewer")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _service.GetByIdAsync(id);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    // =============================
    // GET ALL
    // =============================
    [HttpGet]
    //[Authorize(Roles = "Admin,Manager,Viewer")]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    // =============================
    // UPDATE
    // =============================
    [HttpPatch("{id}/update")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
    {
        if (id != dto.ProductId)
            return BadRequest("Product ID mismatch.");

        var updated = await _service.UpdateAsync(dto);
        return Ok(updated);
    }

    // =============================
    // DELETE (Soft)
    // =============================
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    // =============================
    // HEALTH
    // =============================
    [HttpGet("health")]
    [AllowAnonymous]
    public IActionResult HealthCheck()
        => Ok("Product Service is healthy");

    // =============================
    // PUBLISH
    // =============================
    [HttpPatch("{id:guid}/publish")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Publish(Guid id)
    {
        await _service.PublishAsync(id);
        return NoContent();
    }

    // =============================
    // ADD ATTRIBUTE
    // =============================
    //[HttpPost("{id:guid}/attributes")]
    ////[Authorize(Roles = "Admin,Manager")]
    //public async Task<IActionResult> AddAttribute(Guid id, [FromBody] ProductAttributeValue dto)
    //{
    //    if (id != dto.ProductId)
    //        return BadRequest("Product ID mismatch.");

    //    await _service.AddAttributeAsync(dto.ProductId, dto.AttributeId, dto.Value);
    //    return NoContent();
    //}


}
