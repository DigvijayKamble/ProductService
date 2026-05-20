using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.Property<byte[]>("RowVersion")
               .IsRowVersion();

        builder.OwnsMany(p => p.Pricings, pricing =>
        {
            pricing.OwnsOne(p => p.Price, money =>
            {
                money.Property(m => m.Amount)
                     .HasColumnName("Price");

                money.Property(m => m.Currency)
                     .HasColumnName("CurrencyCode");
            });
        });

        builder.OwnsOne(p => p.SEO, seo =>
        {
            seo.OwnsOne(s => s.Slug, slug =>
            {
                slug.Property(s => s.Value)
                    .HasColumnName("Slug");
            });
        });
    }
}
