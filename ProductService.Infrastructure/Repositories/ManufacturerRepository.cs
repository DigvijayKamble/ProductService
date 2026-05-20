using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.DbContexts;

namespace ProductService.Infrastructure.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
    private readonly ProductDbContext _context;
    public ManufacturerRepository(ProductDbContext context) 
    {
        _context = context;
    }

    public async Task<Manufacturer?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Manufacturers
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }

    public async Task AddAsync(Manufacturer manufacturer, CancellationToken ct)
    {
        await _context.Manufacturers.AddAsync(manufacturer, ct);
    }
}