using BookProject.Controllers.Methods;
using BookProject.Models;
using BookProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BookProject.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepo _bookRepo;
        private readonly IConfiguration Configuration;
        //AutoId autoId;
        BookOperation bookOperation;

        //------------------------------------------------------ //Constructar

        public BookController(IConfiguration _configuration)        
        {
            _bookRepo=new BookRepo();
            Configuration = _configuration;
            bookOperation = new BookOperation(Configuration);
            //autoId = new AutoId(Configuration);
           
        }

        //------------------------------------------------------ //here i called the API

        public async Task<IActionResult> AllBooks()
        {
              
              var books = await bookOperation.AllBooks(); 
              return View(books);

        }

        [Route("Book-Details/{id}", Name = "bookDetails")]
        public async Task<IActionResult> GetBooks(int id)
        {
            BookModel books = await bookOperation.GetBook(id);
            if(books.Title != null)
            { 
                return View(books);
            }
            else
            {
                return Content("There is no Book assigned with the ID-"+id);
            }
        }

        //------------------------------------------------------

        public IActionResult SearchBooks(string bookName,string authorName)
        {
            
            var books = _bookRepo.SearchBooks(bookName,authorName);
            return View(books);
            
        }
        [HttpGet]
        public IActionResult AddNewBook(string result, string id)
        {
            ViewBag.id=id;
            ViewBag.result=result;   
            return View();
        }

        //------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if(ModelState.IsValid)
            {
                string[] s1 = await bookOperation.AddBook(bookModel);
                if (s1[0].Contains("Success"))
                {
                    return RedirectToAction("AddNewBook", new { result = s1[0], id = s1[1] });
                }
                else
                {
                    // return Content("Failed Adding Operation of " + bookModel.Title);
                    ViewBag.id = 0;
                    ViewBag.result = "Failed";
                    return View();
                }
            }
            ViewBag.id = 0;
            ViewBag.result = "Failed";
            return View();

        }
    }
}
