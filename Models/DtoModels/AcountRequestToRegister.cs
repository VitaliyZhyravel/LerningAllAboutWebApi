using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Models.DtoModels
{
    public class AcountRequestToRegister : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone namber")]
        public string PhoneNamber{ get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
              yield return new ValidationResult("Confirm password and password are not equal");
            }
           
        }
    }
}
