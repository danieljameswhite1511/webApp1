using Domain.Common.Entities;
using Domain.Users.Entities;

namespace Domain.Systems.Entities;

public class SystemUser : IAuditedEntity<Guid, Guid>
{
    public Guid Id { get; }
    public Guid SystemId { get; set; }
    public SystemDefinition System { get; set; }
    public Guid UserId { get; set; }
    public IUser User { get; set; }
    
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public DateTime? DeletedDateTime { get; set; }
    public bool IsDeleted { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    public Guid LastModifiedByUserId { get; set; }
    public Guid DeletedByUserId { get; set; }
}