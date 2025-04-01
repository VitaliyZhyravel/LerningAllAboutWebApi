using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace LearningWebApi.CustomAttributes
{
    public class AuthorizeWithBearerScheme : AuthorizeAttribute
    {
        public AuthorizeWithBearerScheme(string? scheme = null, string? roles = null)
        {
            AuthenticationSchemes = scheme ?? JwtBearerDefaults.AuthenticationScheme;
            Roles = roles;
        }
    }
}
