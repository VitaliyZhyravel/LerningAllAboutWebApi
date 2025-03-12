using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.DtoModels
{
    public class CityRequestToCreate
    {
        
        public string Name { get; set; } = string.Empty;
        public Guid? CountryId { get; set; }
    }
}
