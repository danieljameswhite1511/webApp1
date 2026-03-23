using Domain.Systems.Entities;

namespace Domain.Tenants.Entities;

public class Tenant : ITenant {
    public int Id { get; }
    public string? Name { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public DateTime? DeletedDateTime { get; set; }
    public int CreatedByUserId { get; set; }
    public int LastModifiedByUserId { get; set; }
    public int DeletedByUserId { get; set; }
    public bool IsDeleted { get; set; }
    public string DisplayName { get; set; }
    public string UriName { get; set; }
    public List<SystemTenant> SystemTenants { get; set; }
}