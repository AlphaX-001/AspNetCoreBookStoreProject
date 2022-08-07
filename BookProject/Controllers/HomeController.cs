using BookProject.Models;
using BookProject.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookProject.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        [ViewData]
        public string Title { get; set; }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //bookRepo = new BookRepo();
        }
        [Route("~/")]
        public IActionResult Index()
        {
            //var res = bookRepo.GetAllBooks();
            //return View("~/Views/Book/AllBooks.cshtml", res);         /Passing a Mpdel to another view
           
            Title = "Home";  //Passing Title from Controller to Layout using ViewBag Attribute
            return View();
        }
        
        public IActionResult About()
        {
            return View(); 
        }
 


    }
}