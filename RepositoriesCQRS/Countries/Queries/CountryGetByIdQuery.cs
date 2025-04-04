using LearningWebApi.Models.DtoModels;
using MediatR;

namespace LearningWebApi.RepositoriesCQRS.Countries.Queries
{
    public record CountryGetByIdQuery(Guid Id) : IRequest<CountryResponse> { }
}
