using Domain.Systems.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.Users.AppUserEntityConfig;

public class SystemTenantUserEntityConfig : IEntityTypeConfiguration<SystemTenantUser>
{
    public void Configure(EntityTypeBuilder<SystemTenantUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => (AppUser)x.User).WithMany().HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.SystemTenant).WithMany().HasForeignKey(x => x.SystemTenantId);
    }
}