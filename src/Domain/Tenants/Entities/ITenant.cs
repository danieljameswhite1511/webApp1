using Domain.Common.Entities;

namespace Domain.Tenants.Entities;

public interface ITenant : IAuditedEntity<int, int> {
    public string DisplayName { get; set; }
    public string UriName { get; set; }
}