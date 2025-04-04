using LearningWebApi.Models.DtoModels;
using MediatR;

namespace LearningWebApi.RepositoriesCQRS.Countries.Commands
{
    public record CountryDeleteCommand(Guid id) : IRequest<CountryResponse> { }
}
