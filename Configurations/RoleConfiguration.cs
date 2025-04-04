using LearningWebApi.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LearningWebApi.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
           new ApplicationRole
           {
               Id = Guid.Parse("1afeed65-8bee-46e0-b825-8fee33bb1233"),
               Name = "Admin",
               NormalizedName = "ADMIN"
           },
           new ApplicationRole
           {
               Id = Guid.Parse("1afeed65-8bee-46e0-b825-8fee33bb3200"),
               Name = "User",
               NormalizedName = "USER"
           });
        }
    }
}
