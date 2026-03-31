using Domain.Common.Repositories;

namespace Infrastructure.Persistence;

public class UnitOfWork :  IUnitOfWork
{
    private readonly IdentityDbContext _dbContext;
    
    public UnitOfWork(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}