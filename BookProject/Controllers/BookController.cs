using BookProject.Controllers.Methods;
using BookProject.Models;
using BookProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace BookProject.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepo _bookRepo;
        private readonly IConfiguration Configuration;
        //AutoId autoId;
        private IWebHostEnvironment _webhost;
        BookOperation bookOperation;
        LanguageOperation languageOperation;
        //------------------------------------------------------ //Constructar

        public BookController(IConfiguration _configuration, IWebHostEnvironment webHostEnvironment)        
        {
            _bookRepo=new BookRepo();
            Configuration = _configuration;
            bookOperation = new BookOperation(Configuration);
            languageOperation = new LanguageOperation(Configuration);
            _webhost = webHostEnvironment;
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
        public IActionResult AddNewBook(string? result, string? id)
        
        
        {
            ViewBag.id=id;
            ViewBag.result=result;


            //ViewBag.Language = new SelectList(GetLanguage(),"Id","Text");
            ViewBag.Language = languageOperation.AllLang();
            return View();
        }

        //------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if(ModelState.IsValid)
            {
                //if (bookModel.CoverPhoto != null)
                //{
                //    string folder = "BookDetails/CoverPhotos";
                //    string serverfolder = _webhost.WebRootPath;
                //} 

                string[] s1 = await bookOperation.AddBook(bookModel);
                if (s1[0].Contains("Success"))
                {
                    return RedirectToAction("AddNewBook", new { result = s1[0], id = s1[1] });
                }
            }
            ViewBag.Language = languageOperation.AllLang();
            ViewBag.id = 0;
            ViewBag.result = "Failed";
            return View();

        }

        ////This Private Method will Return All the Languages along with their ID's
        //private List<SelectListItem> GetLanguage()
        //{
        //    //var group1 = new SelectListGroup() { Name = "Global Language" };
        //    //var group2 = new SelectListGroup() { Name = "Indian Language" };
        //    //var group3 = new SelectListGroup() { Name = "Foreign Language" };

        //    return new List<SelectListItem>()
        //    {
        //         new SelectListItem
        //        {
        //            Value = "1",
        //            Text ="French",
        //            //Group=group3,

        //        },new SelectListItem
        //        {
        //            Value = "2",
        //            Text ="German",
        //            //Group=group3,
        //        },new SelectListItem
        //        {
        //            Value = "3",
        //            Text ="Bengali",
        //            //Group=group2,
        //        },new SelectListItem
        //        {
        //            Value = "4",
        //            Text ="Dutch",
        //            //Group=group3,
        //        },new SelectListItem
        //        {
        //            Value = "5",
        //            Text ="Spanish",
        //            //Group=group3,
        //        },new SelectListItem
        //        {
        //            Value = "6",
        //            Text ="English",
        //            //Group=group1,
        //        },new SelectListItem
        //        {
        //            Value = "7",
        //            Text ="Hindi",
        //            //Group=group2,
        //        },
        //    };

        //    //return new List<Languages>()
        //    //{
        //    //    new Languages
        //    //    {
        //    //        Id = 1,
        //    //        Text ="French",
        //    //    },new Languages
        //    //    {
        //    //        Id = 2,
        //    //        Text ="German",
        //    //    },new Languages
        //    //    {
        //    //        Id = 3,
        //    //        Text ="Bengali",
        //    //    },new Languages
        //    //    {
        //    //        Id = 4,
        //    //        Text ="Dutch",
        //    //    },new Languages
        //    //    {
        //    //        Id = 5,
        //    //        Text ="Spanish",
        //    //    },new Languages
        //    //    {
        //    //        Id = 6,
        //    //        Text ="English",
        //    //    },new Languages
        //    //    {
        //    //        Id = 7,
        //    //        Text ="Hindi",
        //    //    },
        //    //};

        //}
    }
}
