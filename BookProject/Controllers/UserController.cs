


using BookProject.Controllers.Methods;
using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookProject.Controllers
{
    public class UserController : Controller
    {
        
        IUserOperations _useroperations;
        IAccountManager _accountManager;
        private readonly IConfiguration _configuration;
        
        public UserController(IConfiguration configuration, 
            IUserOperations useroperations, 
            IAccountManager accountManager)
        {
            _configuration = configuration;
            _useroperations = useroperations;
            _accountManager = accountManager;
        }

        [HttpGet]
        [Route("~/Signup",Name ="signup")]
        public IActionResult Signup(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            return View();
        }

        [HttpPost]
        [Route("~/Signup", Name = "signup")]
        public async Task<IActionResult> Signup(NewUserModel user,string? returnUrl)
        {
           if(ModelState.IsValid)
            {
                var res= await _accountManager.CreateUserAsync(user);

                if (!res.Succeeded)
                {
                    foreach (var errors in res.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);
                    }

                }
                else
                {
                    return RedirectToAction("login");
                }
                
            }
           return View();
        }

        [HttpGet]
        [Route("~/Login",Name ="login")]
        public IActionResult Login(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.returnUrl = returnUrl;          
            }
            
            return View();
        }
        [HttpPost]
        [Route("~/login", Name ="login")]
        public async Task<IActionResult> Login(OldUserModel user, string? returnUrl)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountManager.PasswordSignInAsync(user);
                if(result.Succeeded)
                {
                    //Return to previous page after login
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index","Home");
                }
                if(result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Your Email is not Confirmed Yet");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
                
           
            }
            return View();
        }
        [Route("Signout",Name ="signout")]
        public async Task<IActionResult> Signout()
        {
            await _accountManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("~/ChangePassword",Name ="changepassword")]
        [HttpGet]
        public IActionResult ChangePassword(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.returnUrl = returnUrl;
            }
           
            return View();
        }
        [HttpPost]
        [Route("~/ChangePassword", Name = "changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword model,string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result=await _accountManager.ChangePAssword(model);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        ViewBag.returnUrl = returnUrl;
                    }
                    ViewBag.isSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(model);
        }
        //[Route("~/LogIn", Name = "login")]
        //public IActionResult login()
        //{
        //    ViewBag.status = false;
        //    return View();
        //}

        //[HttpPost]
        //[Route("~/LogIn", Name = "login")]
        //public async Task<IActionResult> login(OldUserModel user)
        //{
        //    string res;
        //        res= await _useroperations.LogInUser(user);

        //        if(res.Contains("Success"))
        //        {
        //            //TempData["_isloggedin"] = true;
        //            TempData["user_name"] = "Mr.Bairagi";
        //            return RedirectToAction("Index", "Home");

        //        }
        //    //TempData["_isloggedin"] = false;
        //    ViewBag.status = true;
        //    return View();
        //}
        //[HttpGet]
        //[Route("~/Signup", Name = "signup")]
        //public IActionResult Signup(string? AlertResult)
        //{

        //    ViewBag.alerts = AlertResult;
        //    return View();
        //}

        //[HttpPost]
        //[Route("~/Signup", Name = "signup")]
        //public async Task<IActionResult> Signup(NewUserModel user)
        //{
        //    string result="";
        //    if (ModelState.IsValid)
        //    {    
        //        result = await _useroperations.AddNewUser(user);
        //        if(result.Contains("Success"))
        //        {
        //            return RedirectToAction("login","user");
        //        }
        //    }
        //    ViewBag.alerts = "Failed";
        //    return View();
        //}
    }
}
