using System.ComponentModel.DataAnnotations;

namespace BookProject.Helpers
{
    public class MyCustomValidation:ValidationAttribute
    {
        string _valid;
        public MyCustomValidation(string valid)
        {
               _valid=valid;    
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string authorName=value.ToString();
                if (authorName.Contains(_valid))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Add "+_valid+" after the Title");
            //return new ValidationResult(" errormessage ?? Add "+_valid+" after the Title");
        }
    }
}
