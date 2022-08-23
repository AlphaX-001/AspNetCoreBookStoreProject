
using BookProject.Models;

namespace BookProject.Controllers.Methods
{
    public interface IUserOperations
    {
        Task<string> EncryptPassword(string plainText);
        Task<string> AddNewUser(NewUserModel user);
    }
}