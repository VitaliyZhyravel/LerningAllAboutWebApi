using LearningWebApi.Models.DtoModels;
using MediatR;

namespace LearningWebApi.RepositoriesCQRS.Countries .Queries
{
    public record CountryGetAllQuery : IRequest<List<CountryResponse>> { }
}
