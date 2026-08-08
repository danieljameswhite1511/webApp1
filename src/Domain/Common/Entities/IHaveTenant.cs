namespace Domain.Common.Entities;

public interface IHaveTenant
{
    Guid TenantId { get; set; }
}