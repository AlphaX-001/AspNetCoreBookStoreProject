using BookProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookProject.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepo _bookRepo;
        public BookController()
        {
            _bookRepo=new BookRepo();
        }


        public IActionResult AllBooks()
        {
              var books = _bookRepo.GetAllBooks();
              return View(books);

        }

        [Route("Book-Details/{id}", Name = "bookDetails")]
        public IActionResult GetBooks(int id)
        {
            var books = _bookRepo.GetBooksById(id);
            return View(books);
        }


        public IActionResult SearchBooks(string bookName,string authorName)
        {
            
            var books = _bookRepo.SearchBooks(bookName,authorName);
            return View(books);
            
        }
    }
}
