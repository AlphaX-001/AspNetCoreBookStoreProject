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

        public IActionResult AllBooks()
        {
              
              var books = bookOperation.AllBooks(); 
              return View(books);

        }

        [Route("Book-Details/{id}", Name = "bookDetails")]
        public IActionResult GetBooks(int id)
        {
            BookModel books = bookOperation.GetBook(id);
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

        public IActionResult AddNewBook()
        {
            return View();
        }

        //------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> AddNewBookAsync(BookModel bookModel)
        {

             string s1 = bookOperation.AddBook(bookModel);
            if (s1.Contains("Success"))
            {
                return RedirectToAction("allbooks", "book");
            }
            else
            {
                return Content("Failed Adding Operation of " + bookModel.Title);
            }
        }
    }
}
