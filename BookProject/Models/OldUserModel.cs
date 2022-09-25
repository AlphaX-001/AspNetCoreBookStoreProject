using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class OldUserModel
    {
        [Required,EmailAddress]
        [Display(Name ="Email")]
        public string username { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name ="Remember Me")]
        public bool rememberMe { get; set; }
    }
}
