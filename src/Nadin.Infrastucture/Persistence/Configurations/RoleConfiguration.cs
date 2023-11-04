using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nadin.Domain.Entities;

namespace Nadin.Infrastucture.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.Property(r => r.Title).HasMaxLength(200).IsRequired();
            builder.HasIndex(r => r.Title).IsUnique();
        }
    }
}
