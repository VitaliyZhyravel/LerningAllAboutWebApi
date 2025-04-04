using LearningWebApi.Models.DtoModels;
using MediatR;

namespace LearningWebApi.RepositoriesCQRS.Countries.Commands
{
    public record CountryUpdateCommand(Guid Id, CountryRequestToUpdate requestDto) : IRequest<CountryResponse> { }
}
