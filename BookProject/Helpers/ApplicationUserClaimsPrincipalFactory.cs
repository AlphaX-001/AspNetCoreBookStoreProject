using BookProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BookProject.Helpers
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UsersOfApplication,IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<UsersOfApplication> userManager, 
            IOptions<IdentityOptions> options,
            RoleManager<IdentityRole> rolemanager): 
            base(userManager,rolemanager, options)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UsersOfApplication user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("User_Fullname", user.fullName ?? ""));
            return identity;
        }
    }
}
