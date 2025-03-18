using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly WebApiDataBaseContext dbContext;

        public CountryRepository(WebApiDataBaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Country?>> GetAllAsync()
        {
            return await dbContext.Countries
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(Guid Id)
        {
            return await dbContext.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<Country> CreateAsync(Country country)
        {
            await dbContext.AddAsync(country);

            await dbContext.SaveChangesAsync();
            return country;
        }

        public async Task<Country?> UpdateAsync(Guid id, Country country)
        {
            var countryFromDb = await dbContext.FindAsync<Country>(id);
            if (countryFromDb == null) return null;

            countryFromDb.Name = country.Name;
            countryFromDb.NumberOfPeople = country.NumberOfPeople;
            countryFromDb.Faith = country.Faith;
          


            await dbContext.SaveChangesAsync();

            return countryFromDb;
        }

        public async Task<Country?> DeleteAsync(Guid id)
        {
            var county = await dbContext.Countries.FindAsync(id);

            if (county == null) return null;

            dbContext.Countries.Remove(county);
            await dbContext.SaveChangesAsync();

            return county;
        }
    }
}