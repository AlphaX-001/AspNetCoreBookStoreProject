using BookProject.Models;

namespace BookProject.Repository
{
    public class BookRepo
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }
        public List<BookModel> SearchBooks(string title, string author)
        {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(author)).ToList();
        }
        public BookModel GetBooksById(int id)
        {
            return DataSource().Where(x=>x.Id == id).FirstOrDefault();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel() { Id=1,Title = "Python",Author="Amarlal"},
                new BookModel() { Id=2,Title = "MVC",Author="Anith"},
                new BookModel() { Id=3,Title = "Java",Author="Ani"},
                new BookModel() { Id=4,Title = "DotNet",Author="Nisar"},
                new BookModel() { Id=5,Title = "C#",Author="Laxman"},
                new BookModel() { Id=6,Title = "C++",Author="Sutonu"},
               
            };
        }
    }
}
