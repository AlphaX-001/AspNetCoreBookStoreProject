using BookProject.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly BookRepo bookRepo;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //bookRepo = new BookRepo();
        }

        public IActionResult Index()
        {
            //var res=bookRepo.GetAllBooks();
            //return View("~/Views/Book/AllBooks.cshtml",res);
            return View();
        }
        public IActionResult About()
        {
            return View(); 
        }

    }
}