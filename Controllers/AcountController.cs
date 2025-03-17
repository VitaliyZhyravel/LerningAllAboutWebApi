using AutoMapper;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.Models.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AcountController(IMapper mapper, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser>signInManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AcountRequestToRegister registerRequest)
        {
            if (ModelState.IsValid == false)
            {
               var item = string.Join(Environment.NewLine,ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage));
                return BadRequest(item);
            }
            var newUser = mapper.Map<ApplicationUser>(registerRequest);

            IdentityResult result = await userManager.CreateAsync(newUser, registerRequest.Password);

            if (result.Succeeded == false)
            {
                return BadRequest(result.Errors);
            }
            else
            {
               await signInManager.SignInAsync(newUser,isPersistent : false);
            }
            return RedirectToAction("GetAll","City");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(AcountRequestToLogin login)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest("Incorect password or email");
            }
            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded) 
            {
                return BadRequest("?????");
            }
            return RedirectToAction("GetAll", "City");
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout() 
        {
           await signInManager.SignOutAsync();
            return RedirectToAction("GetAll", "City");
        }
    }
}
