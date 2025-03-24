using LearningWebApi.Models.DomainModels;

namespace LearningWebApi.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City?>> GetAllAsync();
        Task<City?> GetByIdAsyncRepo(Guid Id);
        Task<City> CreateAsync(City city);
        Task<City?> UpdateAsync( Guid id, City city);
        Task<City?> DeleteAsync(Guid id);
    }
}
