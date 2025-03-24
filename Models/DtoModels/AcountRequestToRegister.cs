using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Models.DtoModels
{
    public class AcountRequestToRegister : IValidatableObject
    {
        [Required(ErrorMessage = "{0} can`t be blank")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} can`t be blank")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} can`t be blank")]
        [Phone]
        [Display(Name = "Phone namber")]
        public string PhoneNamber { get; set; }

        [Required(ErrorMessage = "{0} can`t be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} can`t be blank")]
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
