using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly WebApiDataBaseContext dbContext;

        public CityRepository(WebApiDataBaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<City?>> GetAllAsync()
        {
            return await dbContext.Cities
                .AsNoTracking()
                .Include(c => c.Country)
                .ToListAsync();
        }

        public async Task<City?> GetByIdAsyncRepo(Guid Id)
        {
            return await dbContext.Cities
                .AsNoTracking()
                .Include(c => c.Country)
                .FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<City> CreateAsync(City city)
        {
            await dbContext.AddAsync<City>(city);

            await dbContext.SaveChangesAsync();
            return city;
        }

        public async Task<City?> UpdateAsync(Guid id, City city)
        {
            var oldCity = await dbContext.FindAsync<City>(id);
            if (oldCity == null) return null;

            oldCity.Name = city.Name;
            oldCity.CountryId = city.CountryId;

            await dbContext.SaveChangesAsync();

            return oldCity;
        }

        public async Task<City?> DeleteAsync(Guid id)
        {
            var city = await dbContext.Cities.FindAsync(id);

            if (city == null) return null;

            dbContext.Cities.Remove(city);
            await dbContext.SaveChangesAsync();

            return city;
        }
    }
}
