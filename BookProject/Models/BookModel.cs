using BookProject.Enums;
using BookProject.Helpers;
using System.ComponentModel.DataAnnotations;
namespace BookProject.Models
{
    public class BookModel
    {   
        [Required]
        public int Id {  get; set; }

        //[MyCustomValidation("Book")]
        //[MyCustomValidation(errormessage="this is custom error")]
        [Required]
        public string Title { get; set; }

        [Required(ErrorMessage = "You must have to provide the Author name")]      
        public string Author { get; set; }

        [StringLength(500, ErrorMessage ="Description Should not be more than 500 letters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "You must have to provide the total Pages")]
        [Display(Name ="Total Pages of Book")]
        public int? Pages { get; set; }

        public string? Category { get; set; }

        [Display(Name = "Language of the Book")]
        [Required(ErrorMessage ="You must have to select the Language of your Book")]
        public string Language { get; set; }

        //[Required(ErrorMessage = "You must have to select the Languages for your Book")]
        //public List<string> MultiLanguage { get; set; }
        [Display(Name = "Enum: Your Mother Tongue")]
        public LanguageEnum _languageEnum { get; set; }

        public string? UpdatedOn { get; set; }
        public string? CreatedOn { get; set; }

        [Display(Name = "Select the Cover photo of your Book")]
        [Required(ErrorMessage = "You must have to select a cover photo for your Book")]
        public IFormFile CoverImg { get; set; }

        public string coverimgurl { get; set; } = " ";
    }
} 
