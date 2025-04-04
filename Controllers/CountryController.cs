using AutoMapper;
using LearningWebApi.CustomAttributes;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.Repositories;
using LearningWebApi.RepositoriesCQRS.Countries.Commands;
using LearningWebApi.RepositoriesCQRS.Countries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace LearningWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CountryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AuthorizeWithBearerScheme(Roles = "User,Admin")]
        [HttpGet]
        public async Task<ActionResult<CountryResponse>> GetAll()
        {
            return Ok(await mediator.Send(new CountryGetAllQuery()));
        }

        [AuthorizeWithBearerScheme(Roles = "User,Admin")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CountryResponse>> GetById(Guid id)
        {
            return Ok(await mediator.Send(new CountryGetByIdQuery(id)));
        }

        [AuthorizeWithBearerScheme(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CountryResponse>> Create(CountryRequestToCreate request)
        {
            var createdCountry = await mediator.Send(new CountryCreateCommand(request));
            return CreatedAtAction(nameof(GetById), new { id = createdCountry.Id }, createdCountry);
        }

        [AuthorizeWithBearerScheme(Roles = "Admin")]
        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<CountryResponse>> Update([FromRoute] Guid Id, [FromBody] CountryRequestToUpdate request)
        {
            return Ok(await mediator.Send(new CountryUpdateCommand(Id, request)));
        }

        [AuthorizeWithBearerScheme(Roles = "Admin")]
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<CountryResponse>> Delete(Guid id)
        {
            return Ok(await mediator.Send(new CountryDeleteCommand(id)));
        }
    }
}
