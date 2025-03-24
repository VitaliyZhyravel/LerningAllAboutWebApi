using LearningWebApi.Configurations;
using LearningWebApi.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LearningWebApi.Models.EntityModels;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using LearningWebApi.Enums;

namespace LearningWebApi.DataBaseContext;

public class WebApiDataBaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public WebApiDataBaseContext(DbContextOptions<WebApiDataBaseContext> options) : base(options)
    {

    }

    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());

        modelBuilder.Entity<ApplicationRole>()
            .HasData(
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
