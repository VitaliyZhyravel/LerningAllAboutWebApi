using LearningWebApi.Models.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Models.DtoModels
{
    public class CountryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? NumberOfPeople { get; set; }
        public string? Faith { get; set; } = string.Empty;
        public List<City>? Cities { get; set; }
    }
}
