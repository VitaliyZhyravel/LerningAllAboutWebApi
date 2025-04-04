using LearningWebApi.Models.DtoModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebApi.RepositoriesCQRS.Cities.Queries
{
    public record GetAllQuery(string? filterOn , string? filterBy ,
            string? sortBy, bool isAsending ,
          int namberOfPage, int pageSize ) : IRequest<List<CityResponse>>
    { }
}
