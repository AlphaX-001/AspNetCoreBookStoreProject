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
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
       
        
        public AccountManager(IEmailService emailService,
            IConfiguration configuration,
            UserManager<UsersOfApplication> usermanager,
            SignInManager<UsersOfApplication> signinmanager,
            IUserServices userServices)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _userServices = userServices;
            _configuration = configuration;
            _emailService = emailService;
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

            if (result.Succeeded)
            {
                var token = await _usermanager.GenerateEmailConfirmationTokenAsync(user);
                if (!string.IsNullOrEmpty(token))
                {
                    //Here we will call the method which will send ther email along with the token.
                    await SendEmailConfimationMail(user, token);
                }
            }
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

        //For Designing the Email comnfirmation email
        private async Task SendEmailConfimationMail(UsersOfApplication user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                EmailAddresses = new List<string> { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.fullName),

                    new KeyValuePair<string, string>("{{Link}}",
                    String.Format(appDomain+confirmationLink,user.Id,token))
                }
            };
            await _emailService.SendEmailForEmailConfirmation(options);
        }
    }
}
