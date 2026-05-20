using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface IBrandRepository
{
    Task<Brand?> GetByIdAsync(Guid id);
    Task<IEnumerable<Brand>> GetAllAsync();
    Task AddAsync(Brand brand);
    Task UpdateAsync(Brand brand);
}

