using BookProject.Controllers.Methods;
using BookProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookProject.Components
{
    public class SimilarBooksViewComponent : ViewComponent
    {
        IBookOperation _bookOperation;
        public SimilarBooksViewComponent(IConfiguration configuration, IBookOperation bookOperation)
        {
            _bookOperation=bookOperation;
        }
        public async Task<IViewComponentResult> InvokeAsync(string bookCategory)
        {
            var books =await _bookOperation.GetSimilarBooks(bookCategory);
            return View(books);
        }
    }
}
