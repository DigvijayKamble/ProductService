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
public class BrandsController(IBrandBusinessService service) : ControllerBase
{
    private readonly IBrandBusinessService _service = service; 

    [HttpPost]
   // [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create(CreateBrandDto dto)
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
    [HttpGet("{id:Guid}")]
   // [Authorize(Roles = "Admin,Manager,Viewer")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpPut("{id:Guid}")]
   // [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(Guid id, UpdateBrandDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
   // [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
