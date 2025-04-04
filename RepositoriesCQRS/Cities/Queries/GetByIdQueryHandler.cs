using AutoMapper;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Exceptions;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.RepositoriesCQRS.Cities.Queries
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, CityResponse>
    {
        private readonly WebApiDataBaseContext context;
        private readonly IMapper mapper;

        public GetByIdQueryHandler(WebApiDataBaseContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CityResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var city = await context.Cities
                 .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.id);

            if (city == null)
            {
                throw new NotFoundException(nameof(City),request.id);
            }

            return mapper.Map<CityResponse>(city);
               
        }
    }
}
