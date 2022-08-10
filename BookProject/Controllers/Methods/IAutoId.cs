
namespace BookProject.Controllers.Methods
{
    public interface IAutoId
    {
        Task<int> generateId();
    }
}