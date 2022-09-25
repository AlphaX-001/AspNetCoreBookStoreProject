using Microsoft.AspNetCore.Identity;


//This class is created to add Custom column in our ASPNETUSERS table in Identity Framework

namespace BookProject.Models
{
    public class UsersOfApplication : IdentityUser
    {
        public string fullName { get; set; }
    }
}
