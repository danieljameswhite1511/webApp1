using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Systems.Entities.EntityConfigurations;

public class SystemTenantEntityConfig : IEntityTypeConfiguration<SystemTenant> {
    public void Configure(EntityTypeBuilder<SystemTenant> builder) {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId);
        builder.HasOne(x => x.SystemDefinition).WithMany().HasForeignKey(x => x.SystemId);
    }
}