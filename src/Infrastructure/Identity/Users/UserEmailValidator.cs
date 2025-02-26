using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Users;

public class UserEmailValidator : IUserValidator<AppUser>
{
    
    public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
    {
        if (!user.Email.Contains("@"))
        {
            return Task.FromResult(IdentityResult.Failed(new []{new IdentityError{Description="Invalid Email"}}));
        }
        return Task.FromResult(IdentityResult.Success);
    }
}