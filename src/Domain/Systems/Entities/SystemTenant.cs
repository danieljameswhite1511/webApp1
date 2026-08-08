using Domain.Common.Entities;
using Domain.Tenants.Entities;

namespace Domain.Systems.Entities;

public class SystemTenant : IAuditedEntity<Guid, Guid>, IHaveTenant {
    public Guid Id { get; }
    public SystemDefinition SystemDefinition { get; set; }
    public Guid SystemId { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public DateTime? DeletedDateTime { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Guid LastModifiedByUserId { get; set; }
    public Guid DeletedByUserId { get; set; }
    public bool IsDeleted { get; set; }
}