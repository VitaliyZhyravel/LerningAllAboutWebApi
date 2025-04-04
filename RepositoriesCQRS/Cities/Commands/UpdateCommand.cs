using LearningWebApi.Models.DtoModels;
using MediatR;

namespace LearningWebApi.RepositoriesCQRS.Cities.Commands
{
    public record UpdateCommand(Guid id,CityRequestToUpdate requestDto) : IRequest<CityResponse> { }
}
