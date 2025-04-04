using LearningWebApi.Models.DtoModels;
using MediatR;

namespace LearningWebApi.RepositoriesCQRS.Cities.Commands
{
    public record DeleteCommand(Guid id) : IRequest<CityResponse> { }
  
}
