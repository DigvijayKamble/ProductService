using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Outbox;

namespace ProductService.Infrastructure.DbContexts;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options) { }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
    public DbSet<ProductType> ProductTypes => Set<ProductType>();
    public DbSet<Tenant> Tenants => Set<Tenant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
    }

    //public DbSet<Product> Products => Set<Product>();
    //public DbSet<Category> Categories => Set<Category>();
    //public DbSet<Brand> Brands => Set<Brand>();
    ////public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    ////public DbSet<ProductAttributeValue> ProductAttributeValues => Set<ProductAttributeValue>();
    //public DbSet<ProductMedia> ProductMedia => Set<ProductMedia>();
    //public DbSet<Inventory> Inventories => Set<Inventory>();
    //public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    //public DbSet<MeasurementUnit> MeasurementUnits => Set<MeasurementUnit>();

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    // ==========================================================
    //    // PRODUCT
    //    // ==========================================================
    //    modelBuilder.Entity<Product>(e =>
    //    {
    //        e.HasKey(p => p.Id);

    //        e.HasIndex(p => p.SKU)
    //            .IsUnique();

    //        e.Property(p => p.Name)
    //            .IsRequired()
    //            .HasMaxLength(300);

    //        e.Property(p => p.BasePrice)
    //            .HasPrecision(18, 2);

    //        e.Property(p => p.CompareAtPrice)
    //            .HasPrecision(18, 2);

    //        e.Property(p => p.UnitValue)
    //            .HasPrecision(18, 3);

    //        e.HasQueryFilter(p => !p.IsDeleted);

    //        // Backing fields for collections
    //        //e.Metadata.FindNavigation(nameof(Product.Attributes))!
    //        //    .SetPropertyAccessMode(PropertyAccessMode.Field);

    //        e.Metadata.FindNavigation(nameof(Product.Media))!
    //            .SetPropertyAccessMode(PropertyAccessMode.Field);
    //    });

    //    // ==========================================================
    //    // INVENTORY (1:1 with Product)
    //    // ==========================================================
    //    modelBuilder.Entity<Inventory>(e =>
    //    {
    //        e.HasKey(i => i.Id);

    //        e.HasOne<Product>()
    //            .WithOne(p => p.Inventory)
    //            .HasForeignKey<Inventory>(i => i.ProductId)
    //            .OnDelete(DeleteBehavior.Cascade);

    //        e.Property(i => i.QuantityAvailable)
    //            .IsRequired();
    //    });

    //    // ==========================================================
    //    // PRODUCT ATTRIBUTE (MASTER)
    //    // ==========================================================
    //    //modelBuilder.Entity<ProductAttribute>(e =>
    //    //{
    //    //    e.HasKey(a => a.Id);

    //    //    e.Property(a => a.Name)
    //    //        .IsRequired()
    //    //        .HasMaxLength(200);

    //    //    e.Property(a => a.DataType)
    //    //        .IsRequired()
    //    //        .HasMaxLength(50);
    //    //});

    //    // ==========================================================
    //    // PRODUCT ATTRIBUTE VALUE
    //    // ==========================================================
    //    //modelBuilder.Entity<ProductAttributeValue>()
    //    //    .HasOne<AttributeOption>()
    //    //    .WithMany()
    //    //    .HasForeignKey(v => v.AttributeOptionId);

    //    //modelBuilder.Entity<ProductAttributeValue>()
    //    //    .HasOne<Product>()
    //    //    .WithMany(p => p.Attributes)
    //    //    .HasForeignKey(v => v.ProductId);

    //    // ==========================================================
    //    // PRODUCT MEDIA
    //    // ==========================================================
    //    modelBuilder.Entity<ProductMedia>(e =>
    //    {
    //        e.HasKey(m => m.Id);

    //        e.Property(m => m.Url)
    //            .IsRequired()
    //            .HasMaxLength(1000);

    //        e.Property(m => m.MediaType)
    //            .IsRequired()
    //            .HasMaxLength(100);

    //        e.HasOne<Product>()
    //            .WithMany(p => p.Media)
    //            .HasForeignKey(m => m.ProductId)
    //            .OnDelete(DeleteBehavior.Cascade);
    //    });

    //    // ==========================================================
    //    // CATEGORY
    //    // ==========================================================
    //    modelBuilder.Entity<Category>(e =>
    //    {
    //        e.HasKey(c => c.ID);

    //        e.Property(c => c.Name)
    //            .IsRequired()
    //            .HasMaxLength(200);
    //    });

    //    // ==========================================================
    //    // BRAND
    //    // ==========================================================
    //    modelBuilder.Entity<Brand>(e =>
    //    {
    //        e.HasKey(b => b.Id);

    //        e.Property(b => b.Name)
    //            .IsRequired()
    //            .HasMaxLength(200);
    //    });

    //    // ==========================================================
    //    // OUTBOX
    //    // ==========================================================
    //    modelBuilder.Entity<OutboxMessage>(e =>
    //    {
    //        e.HasKey(o => o.Id);

    //        e.Property(o => o.EventType)
    //            .IsRequired()
    //            .HasMaxLength(500);

    //        e.Property(o => o.Payload)
    //            .IsRequired();

    //        e.Property(o => o.IsProcessed)
    //            .HasDefaultValue(false);
    //    });

    //    // ==========================================================
    //    // MEASUREMENT UNIT
    //    // ==========================================================
    //    modelBuilder.Entity<MeasurementUnit>(e =>
    //    {
    //        e.HasKey(x => x.Id);

    //        e.Property(x => x.Name)
    //            .IsRequired()
    //            .HasMaxLength(100);

    //        e.Property(x => x.Symbol)
    //            .IsRequired()
    //            .HasMaxLength(20);

    //        e.HasIndex(x => x.Name)
    //            .IsUnique();
    //    }); 
    //}
}
