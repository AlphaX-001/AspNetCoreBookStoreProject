using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookProject.Controllers.Methods
{
    public interface ILanguageOperation
    {
        List<SelectListItem> AllLang();
        string SelectLang(int id);
    }
}