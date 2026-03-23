using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Systems.Entities.EntityConfigurations;

public class SystemDefinitionEntityConfig : IEntityTypeConfiguration<SystemDefinition>
{
    public void Configure(EntityTypeBuilder<SystemDefinition> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
        builder.HasMany(x => x.SystemTenants).WithOne(x => x.SystemDefinition);
    }
}