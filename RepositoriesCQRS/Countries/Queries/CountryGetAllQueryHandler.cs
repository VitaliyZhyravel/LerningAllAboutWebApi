using AutoMapper;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.DtoModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.RepositoriesCQRS.Countries.Queries
{
    public class CountryGetAllQueryHandler : IRequestHandler<CountryGetAllQuery, List<CountryResponse>>
    {
        private readonly WebApiDataBaseContext context;
        private readonly IMapper mapper;

        public CountryGetAllQueryHandler(WebApiDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<CountryResponse>> Handle(CountryGetAllQuery request, CancellationToken cancellationToken)
        {
            var countries = await context.Countries
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return countries != null ? mapper.Map<List<CountryResponse>>(countries) : new List<CountryResponse>();
        }
    }
}
