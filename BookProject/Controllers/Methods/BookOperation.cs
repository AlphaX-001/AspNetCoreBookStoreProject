using BookProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BookProject.Controllers.Methods
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class BookOperation : ControllerBase, IBookOperation
    {
        IConfiguration Configuration;
        IAutoId _autoId;
        LanguageOperation languageOperations;
        public BookOperation(IConfiguration _configuration, IAutoId autoId)
        {
            Configuration = _configuration;
            _autoId = autoId;
            languageOperations = new LanguageOperation(Configuration);
        }

        public async Task<string[]> AddBook(BookModel bookModel)      //Api For adding books
        {
            int bkid = await _autoId.generateId();
            string[] res = new string[2];
            bookModel.CreatedOn = DateTime.Now.ToString("dd/MM/yyyy - hh:mm tt");
            bookModel.UpdatedOn = DateTime.Now.ToString("dd/MM/yyyy - hh:mm tt");
            string SelectedLanguage = languageOperations.SelectLang(Convert.ToInt32(bookModel.Language));

            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string sqlcmd = "insert into allbook (id,title,author,description,category,pages,language,CreatedOn,UpdatedOn,coverimgurl,bookpdfurl)values" + "('" + bkid + "','" + bookModel.Title + "','" + bookModel.Author + "','" + bookModel.Description + "','" + bookModel.Category + "','" + bookModel.Pages + "','" + SelectedLanguage + "','" + bookModel.CreatedOn + "','" + bookModel.UpdatedOn + "','" + bookModel.coverimgurl + "','" + bookModel.bookpdfurl + "')";
            SqlCommand cmd = new SqlCommand(sqlcmd, conn);
            cmd = new SqlCommand(sqlcmd, conn);
            await cmd.ExecuteNonQueryAsync();
            res[0] = "Success";
            res[1] = bkid.ToString();
            return (res);
        }
        public async Task<string> AddGalleryImg(List<GalleryImgModel> obj)
        {
            string constring = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            foreach (var item in obj)
            {
                string sqlcmd = "INSERT INTO galleryimg(bookid,url)VALUES('" + item.bookid + "','" + item.galleryimgurlurl + "')";
                SqlCommand command = new SqlCommand(sqlcmd, con);
                await command.ExecuteNonQueryAsync();
            }
            con.Close();
            return "Success";
        }

        public async Task<IEnumerable<BookModel>> AllBooks()    //Api for viewing All Books
        {
            List<BookModel> model = new List<BookModel>();
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            string cmd = "select * from allbook";
            SqlCommand sqlCommand = new SqlCommand(cmd, connection);
            SqlDataReader sdr = await sqlCommand.ExecuteReaderAsync();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            foreach (DataRow book in dt.Rows)
            {
                model.Add(
                            new BookModel
                            {
                                Id = Convert.ToInt32(book["id"]),
                                Title = book["title"].ToString(),
                                Author = (book["author"]).ToString(),
                                Description = (book["description"]).ToString(),
                                Category = (book["category"]).ToString(),
                                Pages = Convert.ToInt32(book["pages"]),
                                Language = book["language"].ToString(),
                                coverimgurl = book["coverimgurl"].ToString(),
                            }
                    );

            }
            return model;
        }
        public async Task<List<GalleryImgModel>> AllGalleryImg(int id)
        {
            List<GalleryImgModel> model = new List<GalleryImgModel>();
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            string cmd = "select * from galleryimg where bookid ='" + id + "'";
            SqlCommand sqlCommand = new SqlCommand(cmd, connection);
            SqlDataReader sdr = await sqlCommand.ExecuteReaderAsync();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            foreach (DataRow img in dt.Rows)
            {
                model.Add(new GalleryImgModel()
                {
                    galleryimgurlurl = img["url"].ToString(),
                    id = Convert.ToInt32(img["id"]),
                    bookid = Convert.ToInt32(img["bookid"]),
                });
            }
            return model;
        }


        public async Task<BookModel> GetBook(int id)    //For getting Books by Id
        {
            BookModel model = new BookModel();
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string cmd = "select * from allbook where id = " + id;
            SqlCommand sqlCommand = new SqlCommand(cmd, conn);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            if (dt.Rows.Count > 0)
            {
                DataRow book = dt.Rows[0];

                model.Id = Convert.ToInt32(book["id"]);
                model.Title = book["title"].ToString();
                model.Author = (book["author"]).ToString();
                model.Description = (book["description"]).ToString();
                model.Category = (book["category"]).ToString();
                model.Pages = Convert.ToInt32(book["pages"]);
                model.Language = book["language"].ToString();
                model.CreatedOn = (book["CreatedOn"]).ToString();
                model.UpdatedOn = (book["UpdatedOn"]).ToString();
                model.coverimgurl = (book["coverimgurl"]).ToString();
                model.bookpdfurl = (book["bookpdfurl"]).ToString();
                return model;
            }
            else
            {

                return model;
            }
        }


        public async Task<IEnumerable<BookModel>> GetSimilarBooks(string bookCategory)    //Api for viewing All Books
        {
            List<BookModel> model = new List<BookModel>();
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            string cmd = "select * from allbook where category='" + bookCategory + "'";
            SqlCommand sqlCommand = new SqlCommand(cmd, connection);
            SqlDataReader sdr = await sqlCommand.ExecuteReaderAsync();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            foreach (DataRow books in dt.Rows)
            {
                model.Add(
                            new BookModel
                            {
                                Id = Convert.ToInt32(books["id"]),
                                Title = books["title"].ToString(),
                                Author = (books["author"]).ToString(),
                                Description = (books["description"]).ToString(),
                                Category = (books["category"]).ToString(),
                                Pages = Convert.ToInt32(books["pages"]),
                                Language = books["language"].ToString(),
                                coverimgurl = books["coverimgurl"].ToString(),
                            }
                    );

            }
            return model;
        }
    }
}
