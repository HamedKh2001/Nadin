using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nadin.Domain.Entities;

namespace Nadin.Infrastucture.Persistence.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");
            builder.Property(g => g.IsPermissionBase).HasDefaultValue(false);
            builder.Property(g => g.Caption).HasMaxLength(200).IsRequired();
            builder.HasIndex(g => g.Caption).IsUnique();
        }
    }
}
