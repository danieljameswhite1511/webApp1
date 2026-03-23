using Domain.Common.Entities;

namespace Domain.Systems.Entities;

//Preferred name for this class would be 'System' but this clashes with the existing System namespace
public class SystemDefinition : IAuditedEntity<int, int> {
    public int Id { get; }
    public string? Name { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public DateTime? DeletedDateTime { get; set; }
    public int CreatedByUserId { get; set; }
    public int LastModifiedByUserId { get; set; }
    public int DeletedByUserId { get; set; }
    public bool IsDeleted { get; set; }
    public List<SystemTenant> SystemTenants { get; set; }
}