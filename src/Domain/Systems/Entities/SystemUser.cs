using Domain.Common.Entities;
using Domain.Users.Entities;

namespace Domain.Systems.Entities;

public class SystemUser : IAuditedEntity<int, int>
{
    public int Id { get; }
    public int SystemId { get; set; }
    public SystemDefinition System { get; set; }
    public int UserId { get; set; }
    public IUser User { get; set; }
    
    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public DateTime? DeletedDateTime { get; set; }
    public bool IsDeleted { get; set; }
    
    public int CreatedByUserId { get; set; }
    public int LastModifiedByUserId { get; set; }
    public int DeletedByUserId { get; set; }
}