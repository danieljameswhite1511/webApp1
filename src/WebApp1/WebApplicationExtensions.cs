using System.Linq;
using Infrastructure.Persistence;
using Infrastructure.SeedData;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp1;

public static class WebApplicationExtensions
{
    public static WebApplication SeedApplicationData(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();

        if (!dbContext.Users.Any()) {
            SeedData.SeedUsers(dbContext);
        }
        
        return app;
    }
    
}