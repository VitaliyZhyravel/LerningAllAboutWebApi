using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Models.DtoModels
{
    public class AcountRequestToLogin
    {
        [Required(ErrorMessage = "Email can`t be blank")]
        [EmailAddress(ErrorMessage = "Email shoud be in a proper email adress format")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password can`t be blank")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
