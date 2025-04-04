using Microsoft.AspNetCore.Mvc;
using LearningWebApi.Repositories;
using AutoMapper;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.CustomAttributes;
using LearningWebApi.Filters;
using MediatR;
using LearningWebApi.RepositoriesCQRS.Cities.Queries;
using LearningWebApi.RepositoriesCQRS.Cities.Commands;

namespace LearningWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {

        private readonly IMediator mediator;

        public CityController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [TypeFilter(typeof(ActionFilterForCity))]
        [AuthorizeWithBearerScheme(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<CityResponse>> GetAll([FromQuery] string? filterOn = null, [FromQuery] string? filterBy = null,
            [FromQuery] string? sortBy = null, [FromQuery] bool isAsending = true,
            [FromQuery] int namberOfPage = 1, [FromQuery] int pageSize = 1000)
        {
            var cities = await mediator.Send(new GetAllQuery(filterOn, filterBy,
              sortBy, isAsending, namberOfPage, pageSize));

            return Ok(cities);
        }

        [AuthorizeWithBearerScheme(Roles = "Admin,User")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CityResponse>> GetById([FromRoute] Guid id)
        {
            var product = await mediator.Send(new GetByIdQuery(id));

            return product;
        }

        [HttpPost]
        [AuthorizeWithBearerScheme(Roles = "Admin")]
        public async Task<ActionResult<CityResponse>> Create([FromBody] CityRequestToCreate request)
        {
            var createdCity = await mediator.Send(new CreateCommand(request));

            return CreatedAtAction(nameof(GetById), new { id = createdCity.Id }, createdCity);
        }

        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<CityResponse>> Update([FromRoute] Guid Id, [FromBody] CityRequestToUpdate cityRequest)
        {
            var updatedCity = await mediator.Send(new UpdateCommand(Id, cityRequest));

            return Ok(updatedCity);
        }

        [HttpDelete("{Id:guid}")]
        [AuthorizeWithBearerScheme(Roles = "Admin")]
        public async Task<ActionResult<CityResponse>> Delete([FromRoute] Guid Id)
        {
            var deletedCity = await mediator.Send(new DeleteCommand(Id));

            return Ok(deletedCity);
        }
    }
}

