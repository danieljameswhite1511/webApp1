using Domain.Common.Entities;
using Domain.Tenants.Entities;

namespace Domain.Systems.Entities;

public class SystemTenant : IAuditedEntity<int, int>, IHaveTenant {
    public int Id { get; }
    public SystemDefinition SystemDefinition { get; set; }
    public int SystemId { get; set; }
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public DateTime? DeletedDateTime { get; set; }
    public int CreatedByUserId { get; set; }
    public int LastModifiedByUserId { get; set; }
    public int DeletedByUserId { get; set; }
    public bool IsDeleted { get; set; }
}