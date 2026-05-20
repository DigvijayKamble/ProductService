using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Application.Services;

public class BrandBusinessService(IBrandRepository repository) : IBrandBusinessService
{
    private readonly IBrandRepository _repository = repository;

    public async Task<Guid> CreateAsync(CreateBrandDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.BrandName))
            throw new InvalidOperationException("Brand name is required");

        var brand = new Brand(dto.BrandName);
        await _repository.AddAsync(brand);

        return brand.Id;
    }

    public async Task<IEnumerable<BrandDto>> GetAllAsync()
    {
        var brands = await _repository.GetAllAsync();

        return brands.Select(b => new BrandDto
        {
            Id = b.Id,
            Name = b.Name
        });
    }

    public async Task UpdateAsync(Guid id, UpdateBrandDto dto)
    {
        var brand = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Brand not found");

        brand.Update(dto.Name);
        await _repository.UpdateAsync(brand);
    } 
    public async Task DeleteAsync(Guid id)
    {
        var brand = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Brand not found");

       // brand.MarkDeleted();
        await _repository.UpdateAsync(brand);
    }

    public async Task<BrandDto?> GetByIdAsync(Guid id)
    {
        var brand = await _repository.GetByIdAsync(id);

        if (brand == null)
            return null;

        return new BrandDto
        {
            Id = brand.Id,
            Name = brand.Name
        };
    } 
} 