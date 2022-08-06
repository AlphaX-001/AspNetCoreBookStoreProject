using BookProject.Controllers.Methods;
using BookProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookProject.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        BookOperation bookOperation;
        public TopBooksViewComponent(IConfiguration configuration)
        {
            bookOperation=new BookOperation(configuration);
        }
        public async Task<IViewComponentResult> InvokeAsync(string bookCategory)
        {
            var books =await bookOperation.GetTopBooks(bookCategory);
            return View(books);
        }
    }
}
