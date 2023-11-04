using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nadin.Domain.Entities;

namespace Nadin.Infrastucture.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken");
            builder.Property(r => r.Token).IsRequired();
            builder.HasIndex(r => r.Token).IsUnique();
            builder.Property(r => r.CreatedDate).IsRequired();
            builder.Property(r => r.ExpirationDate).IsRequired();
        }
    }
}
