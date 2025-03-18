using LearningWebApi.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningWebApi.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(new List<City> 
            {
                new City{Id = Guid.Parse("1afeed65-8bee-46e0-b825-8fee33bb8000"), Name = "New Yourk" },
                new City{Id = Guid.Parse("59734acb-bf26-429e-b72e-119284f57e66"), Name = "Kyiv" },
                new City{Id = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), Name = "Beijing" }
            });
        }
    }
}
