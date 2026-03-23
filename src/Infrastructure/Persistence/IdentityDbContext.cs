using Domain.Common.Entities.EntityConfiguration;
using Domain.Systems.Entities;
using Domain.Systems.Entities.EntityConfigurations;
using Domain.Tenants.Entities;
using Domain.Tenants.Entities.EntityConfigurations;
using Infrastructure.Identity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class IdentityDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
    : base(options)
    {
        
    }

    public DbSet<SystemDefinition> Systems { get; set; } 


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new SystemDefinitionEntityConfig().Configure(modelBuilder.Entity<SystemDefinition>()); 
        new SystemTenantEntityConfig().Configure(modelBuilder.Entity<SystemTenant>()); 
        new TenantEntityConfig().Configure(modelBuilder.Entity<Tenant>());
        new SystemTenantUserEntityConfig().Configure(modelBuilder.Entity<SystemTenantUser>());
        
        modelBuilder.ConfigureAuditedEntityModelBuilder();
        
        //
        // var entityTypes = modelBuilder.Model.GetEntityTypes();
        //
        // foreach (var entityType in entityTypes)
        // {
        //     var auditableInterface = entityType.ClrType.GetInterfaces()
        //         .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IAuditedEntity<,>));
        //     if (auditableInterface != null)
        //     {
        //         var builder = modelBuilder.Entity(entityType.ClrType);
        //         builder.Property("CreatedDateTime").HasDefaultValueSql("getdate()").IsRequired(true);
        //         builder.Property("LastModifiedDateTime").HasDefaultValueSql("getdate()").IsRequired(false);
        //         builder.Property("DeletedDateTime").HasDefaultValueSql("getdate()").IsRequired(false);
        //         builder.Property("CreatedByUserId").IsRequired(true);
        //         builder.Property("LastModifiedByUserId").IsRequired(true);
        //         builder.Property("DeletedByUserId").IsRequired(true);
        //         builder.Property("IsDeleted").HasDefaultValue(0).IsRequired(true);
        //         
        //     }
        // }

        
        base.OnModelCreating(modelBuilder);
    }
    
}