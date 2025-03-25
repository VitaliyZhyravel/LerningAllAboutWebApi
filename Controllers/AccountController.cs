using AutoMapper;
using LearningWebApi.Enums;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.Models.EntityModels;
using LearningWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebApi.Controllers
{
    [AllowAnonymous]
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }
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
                IdentityResult res = await userManager.AddToRoleAsync(newUser, Roles.Admin.ToString());

                if (res != null)
                {
                    return BadRequest("Something wrong with role 1");
                }
            }
            else
            {
                IdentityResult res = await userManager.AddToRoleAsync(newUser, Roles.User.ToString());

                if (res == null)
                {
                    return BadRequest("Something wrong with role 2");
                }
            }

            return Ok(new { message = $"User: {newUser.PersonName} registered successfully" });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AcountRequestToLogin loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }

            var user = await userManager.FindByEmailAsync(loginRequest.Email);

            if (user != null)
            {
                var cheackPasswordResult = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (cheackPasswordResult)
                {
                    var token = jwtToken.GenerateJwtToken(user, await userManager.GetRolesAsync(user));

                    Response.Cookies.Append("cokies", token, new CookieOptions
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

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("JwtToken"); 
            return Ok(new { message = "Logout successful" });
        }
    }

}
