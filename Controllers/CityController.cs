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
        public async Task<ActionResult<CityResponse>> GetAll()
        {
            var cities = await cityRepository.GetAllAsync();

            if (cities == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CityResponse>>(cities));
        }

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

        [HttpPost]
        public async Task<ActionResult<CityResponse>> Create(CityRequestToCreate request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var createdCity = await cityRepository.CreateAsync(mapper.Map<City>(request));

            var response = mapper.Map<CityResponse>(createdCity);
            var item = CreatedAtAction(nameof(GetById), controllerName: "City", new { id = response.Id }, response);

            return item;

        }

        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<CityResponse>> Update([FromRoute] Guid Id, [FromBody] CityRequestToUpdate cityRequest)
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

