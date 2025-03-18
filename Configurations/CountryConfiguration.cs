using LearningWebApi.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningWebApi.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(new List<Country>
            {
                new Country{Id = Guid.Parse("4cf2b79e-e2b5-4269-8b6e-c97d1bce34b8"),Name = "Ukraine",NumberOfPeople = 37265521,Faith = "Christianity"},
                new Country{Id = Guid.Parse("910b4080-4425-4e9d-a69c-d2c805f83304"),Name = "United Kindome",NumberOfPeople = 78562981,Faith = "Christianity"},
                new Country{Id = Guid.Parse("c9f9762e-ab79-4c6a-979a-7ca7f3cb6099"),Name = "France",NumberOfPeople = 40678564,Faith = "Christianity"}
            });
        }
    }
}
