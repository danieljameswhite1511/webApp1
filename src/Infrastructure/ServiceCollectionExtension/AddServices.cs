using Domain.auth;
using Domain.Notifications;
using Domain.Repositories;
using Domain.Users;
using Infrastructure.Identity.Auth;
using Infrastructure.Identity.Users;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
using Infrastructure.Urls;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceCollectionExtension;

public static class InfrastructureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserManager<User, int>, AppUserManager>();
        services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IUserDomainService, UserDomainService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<INotification, EmailNotification>();
        services.AddScoped<IUriBuilderService, UriBuilderService>();


    }
}