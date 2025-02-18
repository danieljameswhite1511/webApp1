using System.Text.Json;
using Infrastructure.Identity.Users;
using Infrastructure.Persistence;

namespace Infrastructure.SeedData;

public class SeedData
{
    public static void SeedUsers(ApplicationDbContext dbContext)
    {
        var userData = File.ReadAllText("../Infrastructure/SeedData/Users.json");
        var userDataList = JsonSerializer.Deserialize<List<AppUser>>(userData);
        
        foreach (var user in userDataList)
        {
            dbContext.Users.Add(user);
        }
        
        dbContext.SaveChanges();
        
        
    }
}