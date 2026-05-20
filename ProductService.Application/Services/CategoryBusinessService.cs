using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Application.Services;

public class CategoryBusinessService(ICategoryRepository repository) : ICategoryBusinessService
{
    private readonly ICategoryRepository _repository = repository;
    public async Task<Guid> CreateAsync(CreateCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new InvalidOperationException("Category name is required");

        var category = new Category(dto.Name);
        await _repository.AddAsync(category);

        return category.ID;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();

        return categories.Select(c => new CategoryDto
        {
            Id = c.ID,
            Name = c.Name
        });
    }

    public async Task UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Category not found");

        category.Update(dto.Name);
        await _repository.UpdateAsync(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Category not found");

        //category.MarkDeleted();
        await _repository.UpdateAsync(category);
    }

    public async Task<CategoryDto> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        return new CategoryDto { Id = category.ID, Name = category.Name };

    }
}