using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.DbContexts;

namespace ProductService.Infrastructure.Repositories;

public class TenantRepository : ITenantRepository
{
    private readonly ProductDbContext _context;
    public TenantRepository(ProductDbContext context) 
    {
        _context = context;
    }

    public async Task<Tenant?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Tenants
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task AddAsync(Tenant tenant, CancellationToken ct)
    {
        await _context.Tenants.AddAsync(tenant, ct);
    }
}
