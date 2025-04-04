using AutoMapper;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Exceptions;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.RepositoriesCQRS.Cities.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.RepositoriesCQRS.Countries.Commands
{
    public class CountryUpdateCommandHandler : IRequestHandler<CountryUpdateCommand, CountryResponse>
    {
        private readonly WebApiDataBaseContext context;
        private readonly IMapper mapper;

        public CountryUpdateCommandHandler(WebApiDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CountryResponse> Handle(CountryUpdateCommand request, CancellationToken cancellationToken)
        {
            var countryFromDb = await context.Countries.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (countryFromDb == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            countryFromDb.Name = request.requestDto.Name;
            countryFromDb.NumberOfPeople = request.requestDto.NumberOfPeople;
            countryFromDb.Faith = request.requestDto.Faith;

            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<CountryResponse>(countryFromDb);
        }
    }
}
