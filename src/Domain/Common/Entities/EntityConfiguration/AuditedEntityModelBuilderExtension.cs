using Microsoft.EntityFrameworkCore;

namespace Domain.Common.Entities.EntityConfiguration;

public static class AuditedEntityModelBuilderExtension
{
    public static void ConfigureAuditedEntityModelBuilder(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes();

        foreach (var entityType in entityTypes)
        {
            var auditableInterface = entityType.ClrType.GetInterfaces()
                .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IAuditedEntity<,>));
            if (auditableInterface != null)
            {
                var builder = modelBuilder.Entity(entityType.ClrType);
                builder.Property("CreatedDateTime").HasDefaultValueSql("getdate()").IsRequired(true);
                builder.Property("LastModifiedDateTime").HasDefaultValueSql("getdate()").IsRequired(false);
                builder.Property("DeletedDateTime").HasDefaultValueSql("getdate()").IsRequired(false);
                builder.Property("CreatedByUserId").IsRequired(true);
                builder.Property("LastModifiedByUserId").IsRequired(true);
                builder.Property("DeletedByUserId").IsRequired(true);
                builder.Property("IsDeleted").HasDefaultValue(0).IsRequired(true);
                
            }
        }
    }
}