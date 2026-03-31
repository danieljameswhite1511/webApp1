using Microsoft.EntityFrameworkCore;

namespace Domain.Common.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}