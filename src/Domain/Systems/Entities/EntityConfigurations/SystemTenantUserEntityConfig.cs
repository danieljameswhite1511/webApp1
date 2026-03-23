using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Systems.Entities.EntityConfigurations;

public class SystemTenantUserEntityConfig : IEntityTypeConfiguration<SystemTenantUser>
{
    public void Configure(EntityTypeBuilder<SystemTenantUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.SystemTenant).WithMany().HasForeignKey(x => x.SystemTenantId);
    }
}