using BookProject.Controllers.Methods;
using BookProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookProject.Components
{
    public class SimilarBooksViewComponent : ViewComponent
    {
        BookOperation bookOperation;
        public SimilarBooksViewComponent(IConfiguration configuration)
        {
            bookOperation=new BookOperation(configuration);
        }
        public async Task<IViewComponentResult> InvokeAsync(string bookCategory)
        {
            var books =await bookOperation.GetSimilarBooks(bookCategory);
            return View(books);
        }
    }
}
