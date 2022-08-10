using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookProject.Controllers.Methods
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class LanguageOperation : ControllerBase, ILanguageOperation
    {
        IConfiguration Configuration;
        public LanguageOperation(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public List<SelectListItem> AllLang()
        {
            //List<LanguageModel> model = new List<LanguageModel>();
            List<SelectListItem> lang = new List<SelectListItem>();
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            string sqlcmd = "select * from language";
            SqlConnection sqlconn = new SqlConnection(connectionstring);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand(sqlcmd, sqlconn);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            foreach (DataRow row in dataTable.Rows)
            {
                //model.Add(
                //        new LanguageModel()
                //        {
                //            LangId=Convert.ToInt32(row["langid"]),
                //            LangName=(row["langname"]).ToString(),
                //            langDescription=(row["langdescription"]).ToString(),
                //        }
                //    );
                lang.Add(
                            new SelectListItem()
                            {
                                Text = (row["langname"]).ToString(),
                                Value = (row["langid"]).ToString(),

                            }
                        );
            }
            return lang;
        }
        public string SelectLang(int id)
        {
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            string sqlcmd = "select langname from language where langid='" + id + "'";
            SqlConnection sqlConnection = new SqlConnection(connectionstring);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand(sqlcmd, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["langname"].ToString();
            }
            else
            {
                return " ";
            }
        }
    }

}
