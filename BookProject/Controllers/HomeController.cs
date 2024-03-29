﻿using BookProject.Models;
using BookProject.Repository;
using BookProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace BookProject.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //IConfiguration _configuration;
        IUserServices _userServices;
        private readonly IEmailService _emailService;

        //TestAlertConfig _AlertConfiguration1;
        //TestAlertConfig _AlertConfiguration2;

        [ViewData]
        public string Title { get; set; }
        public HomeController(IOptionsMonitor<TestAlertConfig> AlertConfiguration, IUserServices userServices, IEmailService emailService)
        {
            //_logger = logger;
            //bookRepo = new BookRepo();
            //_configuration = configuration;

            //Named Options for Getting Configuration

            //_AlertConfiguration1 = AlertConfiguration.Get("MicrosoftBook");
            //_AlertConfiguration2 = AlertConfiguration.Get("GoogleBook");
            _userServices=userServices;
            _emailService = emailService;
        }
        [Route("~/{username?}",Name ="homepage")]
        public IActionResult Index()
        {
            //var res = bookRepo.GetAllBooks();                         /Passing a Model to another view
            //return View("~/Views/Book/AllBooks.cshtml", res);         

            //var res = _configuration.GetSection("Test");              /Testing: Fetching Value from appsettings.json file
            //var res2 = res.GetValue<string>("TestValue");

            //var res1 = _AlertConfiguration1.TestValue;
            //var res2 = _AlertConfiguration2.TestValue;
            //ViewBag.username = TempData["user_name"];
            //ViewBag.isloggedin= (TempData["_isloggedin"]==null) ? false : TempData["_isloggedin"];
            Title = "Home";  //Passing Title from Controller to Layout using ViewBag Attribute
            return View();
        }
       
        public async Task<IActionResult> About()
        {
            //It will access the User details
            //var res = _userServices.getUserId();
            //var isLoggedIn = _userServices.isAuthenticated();


            //To Send a test Email ////

            //UserEmailOptions options= new UserEmailOptions
            //{
            //    EmailAddresses = new List<string> { "anithbairagi8@gmail.com" },
            //    Placeholders= new List<KeyValuePair<string, string>>
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}","Jeet")   
            //    }
            //};
            //await _emailService.SendTestEmail(options);

            return View(); 
            
        }
 


    }
}