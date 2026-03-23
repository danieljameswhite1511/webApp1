namespace Domain.Common.Entities;

public interface IHaveTenant
{
    int TenantId { get; set; }
}