using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.DbContexts;

namespace ProductService.Infrastructure.Repositories;

public class ProductTypeRepository: IProductTypeRepository
{
    private readonly ProductDbContext _context;
    public ProductTypeRepository(ProductDbContext context) 
    {
        _context = context;
    }

    public async Task<ProductType?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.ProductTypes
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task AddAsync(ProductType productType, CancellationToken ct)
    {
        await _context.ProductTypes.AddAsync(productType, ct);
    }
}