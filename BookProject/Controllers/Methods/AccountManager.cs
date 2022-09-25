using BookProject.Models;
using BookProject.Services;
using Microsoft.AspNetCore.Identity;

namespace BookProject.Controllers.Methods
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<UsersOfApplication> _usermanager;
        private readonly SignInManager<UsersOfApplication> _signinmanager;
        private readonly IUserServices _userServices;

        public AccountManager(UserManager<UsersOfApplication> usermanager,SignInManager<UsersOfApplication> signinmanager,IUserServices userServices)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _userServices = userServices;
        }
        //For Signup
        public async Task<IdentityResult> CreateUserAsync(NewUserModel userModel)
        {
            var user = new UsersOfApplication()
            {
                Email = userModel.email,
                UserName = userModel.email,
                PhoneNumber = userModel.phone,
                fullName=userModel.fname,
            };
            var result = await _usermanager.CreateAsync(user, userModel.password);
            return result;
        }
        //For Login
        public async Task<SignInResult> PasswordSignInAsync(OldUserModel user)
        {
            var result = await _signinmanager.PasswordSignInAsync(user.username, user.password, user.rememberMe, false);
            return result;
        }
        //For Signout
        public async Task SignOutAsync()
        {
            await _signinmanager.SignOutAsync();
        }
        //For Change Password
        public async Task<IdentityResult> ChangePAssword(ChangePassword model)
        {
            var userid = _userServices.getUserId();
            var user=await _usermanager.FindByIdAsync(userid);
            return await _usermanager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }
    }
}
