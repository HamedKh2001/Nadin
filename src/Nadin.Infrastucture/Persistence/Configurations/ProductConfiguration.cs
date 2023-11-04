using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nadin.Domain.Entities;

namespace Nadin.Infrastucture.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Phone).HasMaxLength(13).IsRequired();
            builder.HasIndex(p => new { p.Email, p.Date }).IsUnique();
            builder.HasOne(p => p.User)
                .WithMany(u => u.Products)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
