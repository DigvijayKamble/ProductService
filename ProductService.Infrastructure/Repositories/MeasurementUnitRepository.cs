using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.DbContexts;

namespace ProductService.Infrastructure.Repositories;

public class MeasurementUnitRepository(ProductDbContext context) : IMeasurementUnitRepository
{
    private readonly ProductDbContext _context = context;
     

    public async Task AddAsync(MeasurementUnit unit)
    {
        await _context.Set<MeasurementUnit>().AddAsync(unit);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MeasurementUnit>> GetAllAsync()
    {
        return await _context.Set<MeasurementUnit>()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<MeasurementUnit?> GetByIdAsync(Guid id)
    {
        return await _context.Set<MeasurementUnit>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
