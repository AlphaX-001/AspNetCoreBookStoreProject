using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class NewUserModel
    {
        public string userid { get; set; }= Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Please enter your Full Name")]
        [Display(Name = "Name")]
        public string fname { get; set; }

        [Required(ErrorMessage = "Please enter your Contact Number")]
        [Display(Name = "Contact No")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Please enter your Email")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Please enter a strong Password")]
        [Display(Name="Password")]
        [MinLength(6)]
        [Compare("confirmpassword", ErrorMessage = "Password does not matched")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Please confirm your Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; }

      

    }
}
