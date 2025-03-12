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

        public CityController(ICityRepository cityRepository, IMapper mapper)
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
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

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<CityResponse>> GetByIdAsync([FromRoute] Guid Id)
        {
            City? city = await cityRepository.GetByIdAsync(Id);

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

            return CreatedAtAction(nameof(GetByIdAsync), new { Id = createdCity.Id }, mapper.Map<CityResponse>(createdCity));
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
        [HttpDelete]
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

