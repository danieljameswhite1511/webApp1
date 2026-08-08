using Domain.Common.Entities;

namespace Domain.Tenants.Entities;

public interface ITenant : IAuditedEntity<Guid, Guid> {
    public string DisplayName { get; set; }
    public string UriName { get; set; }
}