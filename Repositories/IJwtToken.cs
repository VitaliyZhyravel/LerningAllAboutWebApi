using LearningWebApi.Models.EntityModels;

namespace LearningWebApi.Repositories
{
    public interface IJwtToken
    {
        string GenerateJwtToken(ApplicationUser user, IList<string> roles);
    }
}
