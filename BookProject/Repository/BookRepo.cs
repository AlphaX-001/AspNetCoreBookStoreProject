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
                new BookModel() { Id=1,Title = "Python",Author="Amarlal",Description="This is the description of Python Book",Category="Programming",Pages=77,Language="English"},
                new BookModel() { Id=2,Title = "MVC",Author="Anith",Description="This is the description of MVC Book",Category="Technique",Pages=1707,Language="English"},
                new BookModel() { Id=3,Title = "Java",Author="Ani",Description="This is the description of Java Book",Category="Programming Language",Pages=977,Language="English"},
                new BookModel() { Id=4,Title = "DotNet",Author="Nisar",Description="This is the description of DotNet Book",Category="FrameWork",Pages=1477,Language="English"},
                new BookModel() { Id=5,Title = "C#",Author="Laxman",Description="This is the description of C# Book",Category="Programming Language",Pages=537,Language="English"},
                new BookModel() { Id=6,Title = "C++",Author="Sutonu",Description="This is the description of C++ Book",Category="Programming Language",Pages=977,Language="English"},
                new BookModel() { Id=7,Title = "Azure DevOps",Author="Microsoft",Description="This is the description of Azure DevOps Book",Category="Cloud",Pages=1477,Language="English"},

            };
        }
    }
}
