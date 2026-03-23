using System.Linq;
using Infrastructure.Persistence;
using Infrastructure.SeedData;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp1;

public static class WebApplicationExtensions
{
    public static WebApplication SeedIdentityData(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<IdentityDbContext>();
        dbContext.Database.EnsureCreated();

        if (!dbContext.Users.Any()) {
            //dbContext.Users.RemoveRange(dbContext.Users);
            //dbContext.SaveChanges();
            SeedData.SeedUsers(dbContext);
        }
        
        return app;
    }
    
}