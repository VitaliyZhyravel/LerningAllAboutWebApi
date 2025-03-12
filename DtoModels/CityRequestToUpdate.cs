using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.DtoModels
{
    public class CityRequestToUpdate
    {
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public Guid? CountryId { get; set; }
    }
}
