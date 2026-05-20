using ProductService.Application.DTOs;

namespace ProductService.Application.Services; 
public interface IBrandBusinessService
{
    Task<Guid> CreateAsync(CreateBrandDto dto);
    Task<IEnumerable<BrandDto>> GetAllAsync();
    Task<BrandDto?> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateBrandDto dto);
    Task DeleteAsync(Guid id);
}

