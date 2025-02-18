using System.Runtime.CompilerServices;
using Domain.Entities;
using Domain.Repositories;
using Domain.Users;
using Infrastructure.Identity.Users;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceCollectionExtension;

public static class InfrastructureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserManager<User, int>, AppUserManager>();
        services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IUserDomainService, UserDomainService>();
    }
}