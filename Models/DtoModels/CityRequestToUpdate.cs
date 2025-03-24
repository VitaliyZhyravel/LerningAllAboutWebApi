using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Models.DtoModels
{
    public class CityRequestToUpdate
    {
        [MaxLength(30,ErrorMessage ="Max length 30 characters")]
        public string Name { get; set; } = string.Empty;
        public Guid? CountryId { get; set; }
    }
}
