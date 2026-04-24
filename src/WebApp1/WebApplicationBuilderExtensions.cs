using Domain.Common.GlobalConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace WebApp1;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddApplicationConfig(this WebApplicationBuilder builder)
    {
        builder.Configuration.GetSection("SecurityKeys").Bind(ApplicationConfig.SecurityKeys);
        return builder;
    }
}