using AutoMapper;
using LearningWebApi.CustomAttributes;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        [AuthorizeWithBearerScheme(Roles = "User,Admin")]
        [HttpGet]
        public async Task<ActionResult<CountryResponse>> GetAll()
        {
            var country = await countryRepository.GetAllAsync();

            if (country == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CountryResponse>>(country));
        }

        [AuthorizeWithBearerScheme(Roles = "User,Admin")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CountryResponse>> GetById(Guid id)
        {
            Country? country = await countryRepository.GetByIdAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CountryResponse>(country));
        }

        [AuthorizeWithBearerScheme(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CountryResponse>> Create(CountryRequestToCreate request)
        {
            var createdCountry = await countryRepository.CreateAsync(mapper.Map<Country>(request));

            return CreatedAtAction(nameof(GetById), controllerName: "Country", new { id = createdCountry.Id }, mapper.Map<CountryResponse>(createdCountry));
        }

        [AuthorizeWithBearerScheme(Roles = "Admin")]
        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<CountryResponse>> Update([FromRoute] Guid Id, [FromBody] CountryRequestToUpdate countryRequest)
        {
            var updatedCountry = await countryRepository.UpdateAsync(Id, mapper.Map<Country>(countryRequest));
            if (updatedCountry == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CountryResponse>(updatedCountry));
        }

        [AuthorizeWithBearerScheme(Roles = "Admin")]
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<CountryResponse>> Delete(Guid Id)
        {
            var deletedCountry = await countryRepository.DeleteAsync(Id);
            if (deletedCountry == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CountryResponse>(deletedCountry));
        }
    }
}
