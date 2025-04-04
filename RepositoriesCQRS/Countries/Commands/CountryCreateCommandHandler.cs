using AutoMapper;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.RepositoriesCQRS.Countries.Commands
{
    public class CountryCreateCommandHandler : IRequestHandler<CountryCreateCommand, CountryResponse>
    {
        private readonly WebApiDataBaseContext context;
        private readonly IMapper mapper;

        public CountryCreateCommandHandler(WebApiDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<CountryResponse> Handle(CountryCreateCommand request, CancellationToken cancellationToken)
        {
            var newCountry = mapper.Map<Country>(request.requestDto);

            await context.Countries.AddAsync(newCountry, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<CountryResponse>(newCountry);
        }

    }
}
