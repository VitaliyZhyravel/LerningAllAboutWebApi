using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearningWebApi.Models;
using System.Reflection.Metadata.Ecma335;
using LearningWebApi.Repositories;
using LearningWebApi.DtoModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace LearningWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CityController> logger;

        public CityController(ICityRepository cityRepository, IMapper mapper, ILogger<CityController> logger)
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CityResponse>> GetAllAsync()
        {
            var cities = await cityRepository.GetAllAsync();

            if (cities == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CityResponse>>(cities));
        }

        [HttpGet("{id:guid}", Name = "GetByIdAsync")]
        public async Task<ActionResult<CityResponse>> GetByIdAsync(Guid id)
        {
            City? city = await cityRepository.GetByIdAsyncRepo(id);

            if (city == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CityResponse>(city));
        }

        [HttpPost]
        public async Task<ActionResult<CityResponse>> CreateAsync(CityRequestToCreate request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var createdCity = await cityRepository.CreateAsync(mapper.Map<City>(request));

            logger.LogInformation("Created City ID: {CityId}", createdCity.Id);
            logger.LogInformation("Generated URL: {Url}", Url.Action(nameof(GetByIdAsync), new { id = createdCity.Id }));


            var item = CreatedAtAction(nameof(GetByIdAsync), new { id = createdCity.Id }, mapper.Map<CityResponse>(createdCity));

            return item;
                
        }

        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<CityResponse>> UpdateAsync([FromRoute] Guid Id, CityRequestToUpdate cityRequest)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState.Values);
            }
            var updatedCity = await cityRepository.UpdateAsync(Id, mapper.Map<City>(cityRequest));
            if (updatedCity == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CityResponse>(updatedCity));
        }
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<CityResponse>> DeleteAsync(Guid Id)
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

