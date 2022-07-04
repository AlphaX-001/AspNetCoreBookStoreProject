﻿using BookProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BookProject.Controllers.Methods
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class BookOperation : ControllerBase
    {
        IConfiguration Configuration;
        AutoId autoId;
        public BookOperation(IConfiguration _configuration)
        {
            Configuration = _configuration;
            autoId = new AutoId(Configuration);
        }

        public string AddBook(BookModel bookModel)      //Api For adding books
        {
            int bkid = autoId.generateId();
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();

            string sqlcmd = "insert into allbook (id,title,author,description,category,pages,language) values " +
                "('" + bkid + "','" + bookModel.Title + "','" + bookModel.Author + "','" + bookModel.Description + "','" + bookModel.Category + "','" + bookModel.Pages + "','" + bookModel.Language + "')";

            SqlCommand cmd = new SqlCommand(sqlcmd, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            cmd = new SqlCommand(sqlcmd, conn);
            cmd.ExecuteNonQuery();
            string s1 = "Success";
            return s1;
        }

        public IEnumerable<BookModel> AllBooks()    //Api for viewing All Books
        {
            List <BookModel> model = new List<BookModel>();
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            string cmd = "select * from allbook";
            SqlCommand sqlCommand = new SqlCommand(cmd, connection);
            SqlDataReader sdr=sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            foreach(DataRow book in dt.Rows)
            {
                model.Add(
                            new BookModel
                            {
                                Id=Convert.ToInt32(book["id"]),
                                Title=book["title"].ToString(),
                                Author=(book["author"]).ToString(),
                                Description=(book["description"]).ToString(),   
                                Category=(book["category"]).ToString(), 
                                Pages=Convert.ToInt32(book["pages"]),
                                Language=book["language"].ToString(),   

                            }
                    );
                
            }
            return model;
        }
       
        public BookModel GetBook(int id)    //For getting Books by Id
        {
           BookModel model = new BookModel();
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string cmd = "select * from allbook where id = " + id;
            SqlCommand sqlCommand = new SqlCommand(cmd, conn); 
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            if(dt.Rows.Count > 0)
            {
                        DataRow book = dt.Rows[0];

                        model.Id = Convert.ToInt32(book["id"]);
                        model.Title = book["title"].ToString();
                        model.Author = (book["author"]).ToString();
                        model.Description = (book["description"]).ToString();
                        model.Category = (book["category"]).ToString();
                        model.Pages = Convert.ToInt32(book["pages"]);
                        model.Language = book["language"].ToString();

                        return model;
            }
            else
            {

                return model;
            }
        }
    }
}