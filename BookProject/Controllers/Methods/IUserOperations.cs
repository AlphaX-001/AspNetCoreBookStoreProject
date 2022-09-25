//Not Implemented in this Project Because we are using Identity Framework
using BookProject.Models;

namespace BookProject.Controllers.Methods
{
    public interface IUserOperations
    {
        Task<string> EncryptPassword(string plainText);
        Task<string> AddNewUser(NewUserModel user);
        Task<string> LogInUser(OldUserModel user);
    }
}