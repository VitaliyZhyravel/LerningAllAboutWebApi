using Microsoft.AspNetCore.Mvc;
using LearningWebApi.Repositories;
using AutoMapper;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.CustomAttributes;
using LearningWebApi.Filters;

namespace LearningWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExeptionFilterForCityController))]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepository;
        private readonly IMapper mapper;


        public CityController(ICityRepository cityRepository, IMapper mapper)
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }

        [TypeFilter(typeof(ActionFilterForCity))]
        [AuthorizeWithBearerScheme(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<CityResponse>> GetAll([FromQuery] string? filterOn = null, [FromQuery] string? filterBy = null,
            [FromQuery] string? sortBy = null, [FromQuery] bool isAsending = true,
            [FromQuery] int namberOfPage = 1, [FromQuery] int pageSize = 1000)
        {
            var cities = await cityRepository.GetAllAsync(filterOn, filterBy,sortBy,isAsending,namberOfPage,pageSize);

            if (cities == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CityResponse>>(cities));
        }

        [AuthorizeWithBearerScheme(Roles = "Admin,User")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CityResponse>> GetById(Guid id)
        {
            City? city = await cityRepository.GetByIdAsyncRepo(id);

            if (city == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CityResponse>(city));
        }

        [AuthorizeWithBearerScheme(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CityResponse>> Create(CityRequestToCreate request)
        {
            var createdCity = await cityRepository.CreateAsync(mapper.Map<City>(request));

            return CreatedAtAction(nameof(GetById), controllerName: "City", new { id = createdCity.Id }, mapper.Map<CityResponse>(createdCity));
        }

        [AuthorizeWithBearerScheme(Roles = "Admin")]
        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<CityResponse>> Update([FromRoute] Guid Id, [FromBody] CityRequestToUpdate cityRequest)
        {
            var updatedCity = await cityRepository.UpdateAsync(Id, mapper.Map<City>(cityRequest));
            if (updatedCity == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CityResponse>(updatedCity));
        }

        [AuthorizeWithBearerScheme(Roles = "Admin")]
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<CityResponse>> Delete(Guid Id)
        {
            var deletedCity = await cityRepository.DeleteAsync(Id);
            if (deletedCity == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CityResponse>(deletedCity));
        }
    }
}

