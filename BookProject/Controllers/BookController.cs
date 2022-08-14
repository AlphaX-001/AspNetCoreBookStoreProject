using BookProject.Controllers.Methods;
using BookProject.Models;
using BookProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace BookProject.Controllers
{
    public class RandomViewModel
    {
        public BookModel bookmodelreturn { get; set; }
        public List<GalleryImgModel> gallerymodelreturn { get; set; }
    }
    [Route("[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IConfiguration Configuration;
        private IWebHostEnvironment _webhost;

        IBookOperation _bookOperation;
        //ILanguageOperation _languageOperation;
       
        public BookController(IConfiguration _configuration,
            IWebHostEnvironment webHostEnvironment,
            IBookOperation bookOperation,
            ILanguageOperation languageOperation)        
        {
            Configuration = _configuration;
            _bookOperation = bookOperation;
            //_languageOperation = languageOperation;
            _webhost = webHostEnvironment;
            

        }

        //------------------------------------------------------ //here i called the API
        //[Route("~/all-books")]
        public async Task<IActionResult> AllBooks()
        {
              
              var books = await _bookOperation.AllBooks(); 
              return View(books);

        }

        [Route("~/Book-Details/{id}", Name = "bookDetails")]
        public async Task<IActionResult> GetBooks(int id)
        {
            BookModel books = await _bookOperation.GetBook(id);
            if(books.Title != null)
            {
                var randomViewModel = new RandomViewModel() //Done this to pass two models in a view
                {
                    bookmodelreturn = books,
                    gallerymodelreturn = await _bookOperation.AllGalleryImg(id),
                };
                return View(randomViewModel);
            }
            else
            {
                return Content("There is no Book assigned with the ID-"+id);
            }
        }

        //------------------------------------------------------

        //public IActionResult SearchBooks(string bookName,string authorName)
        //{
            
        //    var books = _bookRepo.SearchBooks(bookName,authorName);
        //    return View(books);
            
        //}
        //[Route("~/add-books")]
        [HttpGet]
        public IActionResult AddNewBook(string? result, string? id)        
        {
            ViewBag.id=id;
            ViewBag.result=result;
            //ViewBag.Language = new SelectList(GetLanguage(),"Id","Text");
            //ViewBag.Language = _languageOperation.AllLang();
            return View();
        }

        //------------------------------------------------------
        //[Route("~/add-books")]
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if(ModelState.IsValid)
            {
                
                if (bookModel.CoverImg != null)
                {
                    string folder = "BookDetails/CoverPhotos/";
                    bookModel.coverimgurl = await SaveImg(bookModel.CoverImg, folder);
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "BookDetails/BookPdf/";
                    bookModel.bookpdfurl = await SaveImg(bookModel.BookPdf, folder);
                }
                ///////////////////////////////////////////////////////////// Saving All Data in Database

                string[] s1 = await _bookOperation.AddBook(bookModel);
                string res = "";
                if (bookModel.GalleryImg != null)
                {
                    List<GalleryImgModel> galleryList = new List<GalleryImgModel>();
                    string folder = "BookDetails/GalleryPhotos/";
                    foreach (var item in bookModel.GalleryImg)
                    {
                        galleryList.Add(new GalleryImgModel()
                        {

                            bookid =Convert.ToInt32(s1[1]),
                            galleryimgurlurl = await SaveImg(item, folder),
                        });
                    }
                   res= await _bookOperation.AddGalleryImg(galleryList);
                }
                if (s1[0].Contains("Success") && res.Contains("Success"))
                {
                    return RedirectToAction("AddNewBook", new { result = s1[0], id = s1[1] });
                }
            }
            //ViewBag.Language = _languageOperation.AllLang();
            ViewBag.id = 0;
            ViewBag.result = "Failed";
            return View();

        }

        private async Task<string> SaveImg(IFormFile file,string folder)
        {
            
            folder += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverfolder = Path.Combine(_webhost.WebRootPath, folder);
            //in order to Copy the Image in Project..
            await file.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            return "/" + folder;
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
