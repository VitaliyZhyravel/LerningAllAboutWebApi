using AutoMapper;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Exceptions;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.RepositoriesCQRS.Countries.Queries
{
    public class CountryGetByIdQueryHandler : IRequestHandler<CountryGetByIdQuery, CountryResponse>
    {
        private readonly WebApiDataBaseContext context;
        private readonly IMapper mapper;

        public CountryGetByIdQueryHandler(WebApiDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<CountryResponse> Handle(CountryGetByIdQuery request, CancellationToken cancellationToken)
        {
            var country = await context.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);
                
            if (country == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }

            return mapper.Map<CountryResponse>(country);
        }
    }
}
