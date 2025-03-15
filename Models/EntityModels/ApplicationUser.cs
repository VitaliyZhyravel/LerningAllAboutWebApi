using Microsoft.AspNetCore.Identity;

namespace LearningWebApi.Models.EntityModels
{
    public class ApplicationUser :  IdentityUser<Guid>
    {
        public string PersonName { get; set; }     
    }
}
