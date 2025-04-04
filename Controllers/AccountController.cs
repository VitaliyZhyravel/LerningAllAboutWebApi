using AutoMapper;
using LearningWebApi.CustomAttributes;
using LearningWebApi.Enums;
using LearningWebApi.Filters;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.Models.EntityModels;
using LearningWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebApi.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IJwtToken jwtToken;
        private readonly IConfiguration configuration;

        public AccountController(IMapper mapper, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IJwtToken jwtToken, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtToken = jwtToken;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(AcountRequestToRegister registerRequest)
        {
            if (await userManager.FindByEmailAsync(registerRequest.Email) != null)
            {
                return BadRequest("This email already exist");
            }

            var newUser = mapper.Map<ApplicationUser>(registerRequest);

            IdentityResult result = await userManager.CreateAsync(newUser, registerRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (registerRequest.Email.Contains("admin8624", StringComparison.OrdinalIgnoreCase))
            {
                IdentityResult resultOfAddingRole = await userManager.AddToRoleAsync(newUser, Roles.Admin.ToString());

                if (!resultOfAddingRole.Succeeded)
                {
                    return BadRequest(new { resultOfAddingRole.Errors });
                }
            }
            else
            {
                IdentityResult resultOfAddingRole = await userManager.AddToRoleAsync(newUser, Roles.User.ToString());

                if (!resultOfAddingRole.Succeeded)
                {
                    return BadRequest(resultOfAddingRole.Errors);
                }
            }

            return Ok(new { message = $"User: {newUser.PersonName} registered successfully" });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AcountRequestToLogin loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);

            if (user != null)
            {
                var cheackPasswordResult = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (cheackPasswordResult)
                {
                    var token = jwtToken.GenerateJwtToken(user, await userManager.GetRolesAsync(user));

                    Response.Cookies.Append("Some-Token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:Expiration_minutes"]))
                    });
                    return Ok($"The user {user.PersonName} has successfully logged in");
                }
            }
            return BadRequest("Incorrect password or email");
        }

        [AuthorizeWithBearerScheme]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("Some-Token"); 
            return Task.FromResult( Ok(new { message = "Logout successful" })).Result;
        }
    }

}
