using BookProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace BookProject.Controllers.Methods
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoId : ControllerBase, IAutoId
    {
        IConfiguration Configuration;
        public AutoId(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        string strsql;

        public async Task<int> generateId()
        {
            int maxid;
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");

            strsql = "SELECT MAX(id) as id FROM allbook";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(strsql, con))
                {
                    if (con.State != System.Data.ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    var output = dt.Rows[0]["id"];
                    if (!(output is DBNull))
                    {
                        DataRow data = dt.Rows[0];
                        maxid = Convert.ToInt32(data["id"]);
                        maxid++;
                        return maxid;
                    }
                    else
                    {
                        return 1;
                    }

                }
            }





        }
    }
}
