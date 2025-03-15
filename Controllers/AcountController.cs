using AutoMapper;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.Models.EntityModels;
using Microsoft.AspNetCore.Http;
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

        public AcountController(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(AcountRequestToRegister registerRequest)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await userManager.CreateAsync(mapper.Map<ApplicationUser>(registerRequest),registerRequest.Password);

            if (result.Succeeded == false)
            {
                return BadRequest(result.Errors);
            }
            return RedirectToAction("GetAll","City");
        }
    }
}
