using AutoMapper;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Exceptions;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace LearningWebApi.RepositoriesCQRS.Cities.Commands
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, CityResponse>
    {
        private readonly WebApiDataBaseContext context;
        private readonly IMapper mapper;

        public UpdateCommandHandler(WebApiDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CityResponse> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var cityFromDb = await context.Cities.FirstOrDefaultAsync(x => x.Id == request.id);

            if (cityFromDb == null) 
            {
                throw new NotFoundException(nameof(City),request.id);
            }

            cityFromDb.Name = request.requestDto.Name;
            cityFromDb.CountryId = request.requestDto.CountryId;
            await context.SaveChangesAsync(cancellationToken);

          var cityWithCountry =  context.Cities
               .Include(x => x.Country)
               .Single(x => x.Id == cityFromDb.Id);

            return mapper.Map<CityResponse>(cityWithCountry);
        }
    }
}
