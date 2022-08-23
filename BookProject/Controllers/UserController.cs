


using BookProject.Controllers.Methods;
using BookProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookProject.Controllers
{
    public class UserController : Controller
    {
        IUserOperations _useroperations;
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration, IUserOperations useroperations)
        {
            _configuration=configuration;
            _useroperations=useroperations;
        }

        [HttpGet]
        [Route("~/Signup", Name = "signup")]
        public IActionResult Signup(string? AlertResult)
        {
           
            ViewBag.alerts = AlertResult;
            return View();
        }

        [HttpPost]
        [Route("~/Signup", Name = "signup")]
        public async Task<IActionResult> Signup(NewUserModel user)
        {
            string result="";
            if (ModelState.IsValid)
            {    
                result = await _useroperations.AddNewUser(user);
                if(result.Contains("Success"))
                {
                    return RedirectToAction("signup",result);
                }
            }
            ViewBag.alerts = "Failed";
            return View();
        }
    }
}
