using AutoMapper;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Exceptions;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.RepositoriesCQRS.Countries.Commands
{
    public class CountryDeleteCommandHandler : IRequestHandler<CountryDeleteCommand, CountryResponse>
    {
        private readonly WebApiDataBaseContext context;
        private readonly IMapper mapper;

        public CountryDeleteCommandHandler(WebApiDataBaseContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<CountryResponse> Handle(CountryDeleteCommand request, CancellationToken cancellationToken)
        {
            var countyFromDb = await context.Countries.FirstOrDefaultAsync(x => x.Id == request.id);

            if (countyFromDb == null) 
            {
                throw new NotFoundException(nameof(Country),request.id);
            }
            context.Countries.Remove(countyFromDb);
            await context.SaveChangesAsync();

            return mapper.Map<CountryResponse>(countyFromDb);
        }
    }
}
