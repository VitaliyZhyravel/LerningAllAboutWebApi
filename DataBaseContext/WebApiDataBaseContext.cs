using LearningWebApi.Configurations;
using LearningWebApi.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LearningWebApi.Models.EntityModels;

namespace LearningWebApi.DataBaseContext;

public class WebApiDataBaseContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
{
    public WebApiDataBaseContext(DbContextOptions<WebApiDataBaseContext> options) : base(options) 
    {

    }
    
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Counties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration<City>(new CityConfiguration());
    }

}
