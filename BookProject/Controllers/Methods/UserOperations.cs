//Not Implemented in this Project Because we are using Identity Framework
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Security.Cryptography;
using BookProject.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BookProject.Controllers.Methods
{
    [ApiController]
    [Route("api/[controller]/[action]/{id?}")]
    public class UserOperations : ControllerBase, IUserOperations
    {
        IConfiguration _configuration;
        public UserOperations(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //Password Encrypter
        public async Task<string> EncryptPassword(string plainText)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }
        //Restrict Duplicate Entry
        public async Task<bool> IsDuplicate(NewUserModel user)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string connectionstring = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string sqlcmd1 = "SELECT * FROM BookAppUsers where email='"+user.email+"'";
            string sqlcmd2 = "";
            /*"SELECT * FROM BookAppUsers where username='"+user.name+"'";*/

            SqlCommand sqlCommand1 = new SqlCommand(sqlcmd1,conn);
            SqlCommand sqlCommand2 = new SqlCommand(sqlcmd2,conn);

            SqlDataReader sdr1 = await sqlCommand1.ExecuteReaderAsync();
            dt1.Load(sdr1);
            SqlDataReader sdr2 = await sqlCommand2.ExecuteReaderAsync();
            dt2.Load(sdr2);
            if(dt1.Rows.Count > 0 || dt2.Rows.Count>0)
            {
                return true;
            }
            return false;
        }
        //Add user
        public async Task<string> AddNewUser(NewUserModel user)
        {
            if (await IsDuplicate(user) == false)
            {
                user.userid = Guid.NewGuid().ToString();
                string _password = await EncryptPassword(user.password);
                string connectionstring = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                string sqlcmd = "";
                    //"INSERT INTO BookAppUsers(id,username,email,passwordhash) VALUES('" + user.userid + "','" + user.name + "','" + user.email + "','" + _password + "')";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                await cmd.ExecuteNonQueryAsync();
                return "Success";
            }
            return "Registered";
        }
        //Login
        public async Task<string> LogInUser(OldUserModel user)
        {
            string res = "Success";
            string _password = await EncryptPassword(user.password);
            string connectionstring = _configuration.GetConnectionString("DefaultConnection");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string sqlcmd = "select * from BookAppUsers where email='" + user.username + "' and passwordhash='"+ _password + "'"; 
            SqlCommand cmd = new SqlCommand(sqlcmd,conn);
            SqlDataReader sqlDataReader = await cmd.ExecuteReaderAsync();  
            dt.Load(sqlDataReader);
            if (dt.Rows.Count > 0)
            {   
               //Take username from here
                return res;
            }
            else
            {
                return "Failed";
            }
        }

    }
}
