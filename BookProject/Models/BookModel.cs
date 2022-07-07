using System.ComponentModel.DataAnnotations;
namespace BookProject.Models
{
    public class BookModel
    {
        public int Id {  get; set; }

        [Required(ErrorMessage ="You must have to provide a Title of your Book")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You must have to provide a Title of your Book")]
        public string Author { get; set; }
        [StringLength(500,ErrorMessage ="Description Should not be more than 500 letters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "You must have to provide the total pages of your Book")]
        [Display(Name ="Total Pages of Book")]
        public int? Pages { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
} 
