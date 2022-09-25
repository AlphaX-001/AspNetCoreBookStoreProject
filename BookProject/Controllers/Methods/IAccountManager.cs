using BookProject.Models;
using Microsoft.AspNetCore.Identity;

namespace BookProject.Controllers.Methods
{
    public interface IAccountManager
    {
        Task<IdentityResult> CreateUserAsync(NewUserModel userModel);
        Task<SignInResult> PasswordSignInAsync(OldUserModel user);
        Task SignOutAsync();
        Task<IdentityResult> ChangePAssword(ChangePassword model);
    }
}