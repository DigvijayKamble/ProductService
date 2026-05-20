using ProductService.Application.DTOs;

namespace ProductService.Application.Services; 
public interface ICategoryBusinessService
{
    Task<Guid> CreateAsync(CreateCategoryDto dto);
    Task<IEnumerable<CategoryDto>> GetAllAsync(); 
    Task<CategoryDto> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateCategoryDto dto);
    Task DeleteAsync(Guid id);
}
