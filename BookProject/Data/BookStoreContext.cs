using BookProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Data
{
    public class BookStoreContext : IdentityDbContext<UsersOfApplication>
    {
        public BookStoreContext(DbContextOptions options) : base(options)
        {

        }
    }
}
