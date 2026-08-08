using Domain.Common.GlobalConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp1;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddApplicationConfig(this WebApplicationBuilder builder)
    {
        // this way makes it accessiable via applicationconfig.section.whatever..
        //builder.Configuration.GetSection("SecurityKeys").Bind(ApplicationConfig.SecurityKeys);
        
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        builder.Services.Configure<SecurityKeys>(builder.Configuration.GetSection("SecurityKeys"));
        
        return builder;
    }
}