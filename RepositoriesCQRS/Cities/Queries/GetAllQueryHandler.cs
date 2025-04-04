using AutoMapper;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Globalization;

namespace LearningWebApi.RepositoriesCQRS.Cities.Queries
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<CityResponse>>
    {
        private readonly WebApiDataBaseContext context;
        private readonly IMapper mapper;

        public GetAllQueryHandler(WebApiDataBaseContext conetxt, IMapper mapper)
        {
            context = conetxt;
            this.mapper = mapper;
        }
        public async Task<List<CityResponse>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var cities = mapper.Map<List<CityResponse>>(await context.Cities
                    .AsNoTracking()
                    .Include(x => x.Country)
                    .ToListAsync());

            if (cities == null)
            {
                return new List<CityResponse>();
            }

            if (!string.IsNullOrWhiteSpace(request.filterOn) && !string.IsNullOrWhiteSpace(request.filterBy))
            {
                if (request.filterOn.Equals(nameof(City.Name), StringComparison.OrdinalIgnoreCase))
                {
                    cities = cities.Where(x => x.Name.Contains(request.filterBy, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }

            if (!string.IsNullOrWhiteSpace(request.sortBy))
            {
                if (request.sortBy.Equals(nameof(City.Name), StringComparison.OrdinalIgnoreCase))
                {
                    cities = request.isAsending ? cities.OrderBy(x => x.Name).ToList() : cities.OrderByDescending(x => x.Name).ToList();
                }
            }

            cities = cities.Skip((request.namberOfPage - 1) * request.pageSize).Take(request.pageSize).ToList();


            return cities;
        }
    }
}
