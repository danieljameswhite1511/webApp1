using Domain.Systems.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.Users.AppUserEntityConfig;

public class SystemUserEntityConfig : IEntityTypeConfiguration<SystemUser> 
{
    public void Configure(EntityTypeBuilder<SystemUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(s => s.System).WithMany().HasForeignKey(s => s.SystemId);
        builder.HasOne(x => (AppUser)x.User).WithMany().HasForeignKey(s => s.UserId);
    }
}