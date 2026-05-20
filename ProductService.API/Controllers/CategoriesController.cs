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
public class CategoriesController(ICategoryBusinessService service) : ControllerBase
{
    private readonly ICategoryBusinessService _service = service; 

    [HttpPost]
    //[Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return Ok(id);
    }

    [HttpGet]
    //[Authorize(Roles = "Admin,Manager,Viewer")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPut("{id:Guid}")]
    //[Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    //[Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id:Guid}")] 
    //[Authorize(Roles = "Admin,Manager,Viewer")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
}

