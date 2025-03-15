using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Models.DtoModels
{
    public class CityRequestToCreate
    {
        [MaxLength(30 ,ErrorMessage = "{0} must be a maximum length of 30")]
        public string Name { get; set; } = string.Empty;
        public Guid? CountryId { get; set; }
    }
}
