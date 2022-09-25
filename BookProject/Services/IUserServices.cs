namespace BookProject.Services
{
    public interface IUserServices
    {
        string getUserId();
        bool isAuthenticated();
    }
}