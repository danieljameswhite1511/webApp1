using Application.Auth;
using Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application.ServiceCollectionExtensions;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IAuthAppService, AuthAppService>();
        return services;
    }
}