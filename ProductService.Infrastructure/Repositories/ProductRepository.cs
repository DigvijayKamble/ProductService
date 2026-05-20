using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.DbContexts;

namespace ProductService.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product.Id;
    }

    public async Task<Product?> GetByIdAsync(Guid id, bool includeUnpublished = false)
    {
        var query = _context.Products
            .Include(p => p.Inventory)
            .Include(p => p.Media)
            //.Include(p => p.Attributes)
            //.ThenInclude(a => a.AttributeOptionId)
            .AsQueryable();

        if (!includeUnpublished)
            query = query.Where(p => p.IsPublished);

        return await query.FirstOrDefaultAsync(p => p.Id == id);

    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Media)
            .Where(p => p.IsActive && !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
    //// ======================================================
    //// ADD ATTRIBUTE MASTER
    //// ======================================================
    //public async Task AddProductAttributeAsync(ProductAttribute attribute)
    //{
    //    await _context.ProductAttributes.AddAsync(attribute);
    //    await _context.SaveChangesAsync();
    //}

    //// ======================================================
    //// ADD ATTRIBUTE OPTION
    //// ======================================================
    //public async Task AddAttributeOptionAsync(AttributeOption option)
    //{
    //    await _context.Set<AttributeOption>().AddAsync(option);
    //    await _context.SaveChangesAsync();
    //}

    // ======================================================
    // ADD PRODUCT ATTRIBUTE VALUE (Mapping)
    // ======================================================
    //public async Task AddProductAttributeValueAsync(ProductAttributeValue value)
    //{
    //    await _context.ProductAttributeValues.AddAsync(value);
    //    await _context.SaveChangesAsync();
    //}

}
