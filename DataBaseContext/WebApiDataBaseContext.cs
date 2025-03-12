using LearningWebApi.Configurations;
using LearningWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.DataBaseContext;

public class WebApiDataBaseContext : DbContext
{
    public WebApiDataBaseContext(DbContextOptions options) : base(options) 
    {

    }
    
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Counties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<City>(new CityConfiguration());
    }

}
