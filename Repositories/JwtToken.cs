using LearningWebApi.Models.EntityModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearningWebApi.Repositories
{
    public class JwtToken : IJwtToken
    {
        private readonly IConfiguration configuration;

        public JwtToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Унікальний ідентифікатор
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Унікальний токен
                new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64), // Коректний формат
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Ідентифікатор користувача
                new Claim(ClaimTypes.Email, user.Email), // Email
                new Claim(ClaimTypes.GivenName, user.PersonName) // Ім'я
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey securitykKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            SigningCredentials signingCredentials = new SigningCredentials(securitykKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                 issuer: configuration["Jwt:Issuer"],
                 audience: configuration["Jwt:Audience"],
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:Expiration_minutes"])),
                 signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

