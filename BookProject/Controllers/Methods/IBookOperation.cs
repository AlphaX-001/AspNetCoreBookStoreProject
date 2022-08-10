using BookProject.Models;

namespace BookProject.Controllers.Methods
{
    public interface IBookOperation
    {
        Task<string[]> AddBook(BookModel bookModel);
        Task<string> AddGalleryImg(List<GalleryImgModel> obj);
        Task<IEnumerable<BookModel>> AllBooks();
        Task<List<GalleryImgModel>> AllGalleryImg(int id);
        Task<BookModel> GetBook(int id);
        Task<IEnumerable<BookModel>> GetSimilarBooks(string bookCategory);
    }
}