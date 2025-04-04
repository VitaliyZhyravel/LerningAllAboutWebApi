using AutoMapper;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using MediatR;

namespace LearningWebApi.RepositoriesCQRS.Cities.Commands
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, CityResponse>
    {
        private readonly WebApiDataBaseContext context;
        private readonly IMapper mapper;

        public CreateCommandHandler(WebApiDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<CityResponse> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var newCity = mapper.Map<City>(request.request);

            await context.AddAsync(newCity);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<CityResponse>(newCity);
        }
    }
}
