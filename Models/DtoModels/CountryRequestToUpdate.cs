using LearningWebApi.Models.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Models.DtoModels
{
    public class CountryRequestToUpdate
    {
        [MaxLength(50, ErrorMessage = "Max length 50 characters")]
        public string Name { get; set; } = string.Empty;
        public int? NumberOfPeople { get; set; }
        [MaxLength(50, ErrorMessage = "Max length 50 characters")]
        public string? Faith { get; set; } = string.Empty;
    }
}
