using LearningWebApi.Models.DtoModels;
using MediatR;

namespace LearningWebApi.RepositoriesCQRS.Cities.Queries
{
    public record GetByIdQuery(Guid id) : IRequest<CityResponse> { }
}
