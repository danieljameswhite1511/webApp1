using Domain.Common.Entities;

namespace Domain.auth.RefreshTokens;

public class RefreshToken : IAuditedEntity<Guid, Guid>
{
    public Guid Id { get; }
    public string UserId { get; set; } = string.Empty;
    public string EncryptedToken { get; set; } = string.Empty; 
    public string JwtId { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime ExpiryDate { get; set; }
    public bool Used { get; set; } = false;
    public bool Revoked { get; set; } = false;
    public string? ReplacedByToken { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
    public bool IsActive => !Revoked && !Used && !IsExpired;

    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public DateTime? DeletedDateTime { get; set; }
    public bool IsDeleted { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    public Guid LastModifiedByUserId { get; set; }
    public Guid DeletedByUserId { get; set; }
}