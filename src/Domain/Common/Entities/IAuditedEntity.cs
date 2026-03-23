using Domain.Entities;

namespace Domain.Common.Entities
{
    public interface IAuditedEntity<TPrimary, TUserId> : IAuditedEntity, IEntity<TPrimary>
        where TPrimary : IEquatable<TPrimary>
    {
        public TUserId CreatedByUserId { get; set; }
        public TUserId LastModifiedByUserId { get; set; }
        public TUserId DeletedByUserId { get; set; }
        
    }
}

namespace Domain.Entities
{
    public interface IAuditedEntity
    {
        DateTime CreatedDateTime { get; set; }
        DateTime? LastModifiedDateTime { get; set; }
        DateTime? DeletedDateTime { get; set;}
        public bool IsDeleted { get; set; }   
    }
}