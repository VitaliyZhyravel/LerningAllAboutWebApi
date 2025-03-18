using LearningWebApi.Models.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Models.DtoModels
{
    public class CountryRequestToUpdate
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public int? NumberOfPeople { get; set; }
        [MaxLength(50)]
        public string? Faith { get; set; } = string.Empty;
    }
}
