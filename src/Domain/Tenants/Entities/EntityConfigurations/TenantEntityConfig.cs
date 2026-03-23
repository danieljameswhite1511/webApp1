using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Tenants.Entities.EntityConfigurations;

public class TenantEntityConfig : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(256);
        builder.Property(x => x.UriName).IsRequired().HasMaxLength(256);
        builder.HasMany(x => x.SystemTenants).WithOne(x => x.Tenant);

    }
}