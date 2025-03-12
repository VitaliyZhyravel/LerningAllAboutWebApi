using LearningWebApi.DtoModels;
using LearningWebApi.Models;

namespace LearningWebApi.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City?>> GetAllAsync();
        Task<City?> GetByIdAsync(Guid Id);
        Task<City> CreateAsync(City city);
        Task<City?> UpdateAsync( Guid id, City city);
        Task<City?> DeleteAsync(Guid id);
        
    }
}
