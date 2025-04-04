using LearningWebApi.Models.DtoModels;
using MediatR;

namespace LearningWebApi.RepositoriesCQRS.Countries.Commands
{
    public record CountryCreateCommand(CountryRequestToCreate requestDto) : IRequest<CountryResponse> { }
}
