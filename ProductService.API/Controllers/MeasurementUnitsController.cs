using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Application.Services;

namespace ProductService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/measurement-units")]
public class MeasurementUnitsController : ControllerBase
{
    private readonly IMeasurementUnitService _service;

    public MeasurementUnitsController(IMeasurementUnitService service)
    {
        _service = service;
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMeasurementUnitDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var units = await _service.GetAllAsync();
        return Ok(units);
    }

    // GET BY ID
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var unit = await _service.GetByIdAsync(id);

        if (unit == null)
            return NotFound();

        return Ok(unit);
    }
}