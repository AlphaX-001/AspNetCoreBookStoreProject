//This class is screated to create some basic methods to get user details inside the application
using System.Security.Claims;

namespace BookProject.Services
{
    public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserServices(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string getUserId()
        {
            return _contextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public bool isAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
