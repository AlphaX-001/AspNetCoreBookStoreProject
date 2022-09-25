using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class ChangePassword
    {
        [Required, DataType(DataType.Password),Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "Confirm New Password"),Compare("NewPassword",ErrorMessage ="Confirm Does not Match")]
        public string ConfirmPassword { get; set; }
    }
}
