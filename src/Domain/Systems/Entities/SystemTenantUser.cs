using Domain.Common.Entities;
using Domain.Users.Entities;

namespace Domain.Systems.Entities;

public class SystemTenantUser : IAuditedEntity<int, int> {
    public int Id { get; }
    public int SystemTenantId { get; set; }
    public SystemTenant SystemTenant { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public DateTime? DeletedDateTime { get; set; }
    public int CreatedByUserId { get; set; }
    public int LastModifiedByUserId { get; set; }
    public int DeletedByUserId { get; set; }
    public bool IsDeleted { get; set; }
}