using LearningWebApi.Models.DomainModels;

namespace LearningWebApi.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country?>> GetAllAsync();
        Task<Country?> GetByIdAsync(Guid Id);
        Task<Country> CreateAsync(Country country);
        Task<Country?> UpdateAsync(Guid id, Country country);
        Task<Country?> DeleteAsync(Guid id);
    }
}
