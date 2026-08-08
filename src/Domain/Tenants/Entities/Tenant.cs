using Domain.Systems.Entities;

namespace Domain.Tenants.Entities;

public class Tenant : ITenant {
    public Guid Id { get; }
    public string? Name { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public DateTime? DeletedDateTime { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Guid LastModifiedByUserId { get; set; }
    public Guid DeletedByUserId { get; set; }
    public bool IsDeleted { get; set; }
    public string DisplayName { get; set; }
    public string UriName { get; set; }
    public List<SystemTenant> SystemTenants { get; set; }
}