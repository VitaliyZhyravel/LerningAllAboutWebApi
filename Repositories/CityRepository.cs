using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.DomainModels;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<IEnumerable<City?>> GetAllAsync(string? filterOn, string? filterBy,
            string? sortBy, bool isAsending,
            int namberOfPage, int pageSize)
        {
            var cities = await dbContext.Cities
                  .AsNoTracking()
                  .Include(c => c.Country)
                  .ToListAsync();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterBy))
            {
                if (filterOn.Equals(nameof(City.Name), StringComparison.OrdinalIgnoreCase))
                {
                    cities = cities.Where(x => x.Name.Contains(filterBy,StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals(nameof(City.Name), StringComparison.OrdinalIgnoreCase))
                {
                    cities = isAsending ? cities.OrderBy(x => x.Name).ToList() : cities.OrderByDescending(x => x.Name).ToList();
                }
            }

            return cities.Skip((namberOfPage - 1) * pageSize).Take(pageSize);
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
            var cityFromDb = await dbContext.FindAsync<City>(id);
            if (cityFromDb == null) return null;

            cityFromDb.Name = city.Name;
            cityFromDb.CountryId = city.CountryId;

            await dbContext.SaveChangesAsync();

            return cityFromDb;
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
