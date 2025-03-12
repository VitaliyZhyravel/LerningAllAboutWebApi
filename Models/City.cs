using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningWebApi.Models
{
    public class City
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [ForeignKey("CounryId")]
        public Guid? CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
