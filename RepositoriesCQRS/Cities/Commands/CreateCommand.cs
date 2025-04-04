using LearningWebApi.Models.DtoModels;
using MediatR;

namespace LearningWebApi.RepositoriesCQRS.Cities.Commands
{
    public record CreateCommand(CityRequestToCreate request) : IRequest<CityResponse> { }
}
