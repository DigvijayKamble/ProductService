using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.DbContexts;

namespace ProductService.Infrastructure.Repositories;

public class BrandRepository(ProductDbContext context) : IBrandRepository
{
    private readonly ProductDbContext _context = context;


    public async Task AddAsync(Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
    }

    public async Task<Brand?> GetByIdAsync(Guid id)
    {
        return await _context.Brands
            .FirstOrDefaultAsync(x => x.Id == id);// && !x.IsActive);
    }

    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        return await _context.Brands
            //.Where(x => !x.IsActive)
            .ToListAsync();
    }

    public async Task UpdateAsync(Brand brand)
    {
        _context.Brands.Update(brand);
        await _context.SaveChangesAsync();
    }

}

