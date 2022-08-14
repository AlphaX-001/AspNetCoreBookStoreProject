using BookProject.Models;
using BookProject.Repository;
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

        //TestAlertConfig _AlertConfiguration1;
        //TestAlertConfig _AlertConfiguration2;

        [ViewData]
        public string Title { get; set; }
        public HomeController(IOptionsMonitor<TestAlertConfig> AlertConfiguration)
        {
            //_logger = logger;
            //bookRepo = new BookRepo();
            //_configuration = configuration;
                                
                        //Named Options for Getting Configuration

            //_AlertConfiguration1 = AlertConfiguration.Get("MicrosoftBook");
            //_AlertConfiguration2 = AlertConfiguration.Get("GoogleBook");
            
            
        }
        [Route("~/")]
        public IActionResult Index()
        {
            //var res = bookRepo.GetAllBooks();                         /Passing a Model to another view
            //return View("~/Views/Book/AllBooks.cshtml", res);         

            //var res = _configuration.GetSection("Test");              /Testing: Fetching Value from appsettings.json file
            //var res2 = res.GetValue<string>("TestValue");

            //var res1 = _AlertConfiguration1.TestValue;
            //var res2 = _AlertConfiguration2.TestValue;

            Title = "Home";  //Passing Title from Controller to Layout using ViewBag Attribute
            return View();
        }
       
        public IActionResult About()
        {
            return View(); 
        }
 


    }
}