using AutoMapper;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.Models.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        public AccountController(IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("registerLogika")]
        public async Task<IActionResult> Register(AcountRequestToRegister registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }

            var newUser = mapper.Map<ApplicationUser>(registerRequest);
            IdentityResult result = await userManager.CreateAsync(newUser, registerRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await signInManager.SignInAsync(newUser, isPersistent: false);
            return Ok(new { message = "User registered successfully" });
        }

        [AllowAnonymous]
        [HttpPost("loginLogika")]
        public async Task<IActionResult> Login(AcountRequestToLogin login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Incorrect email or password");
            }

            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return BadRequest("Incorrect email or password");
            }

            return Ok(new { message = "Login successful" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }
    }

}
