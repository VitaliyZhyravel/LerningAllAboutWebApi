using AutoMapper;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Exceptions;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearningWebApi.RepositoriesCQRS.Cities.Commands
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, CityResponse>
    {
        private readonly WebApiDataBaseContext context;
        private readonly IMapper mapper;

        public DeleteCommandHandler(WebApiDataBaseContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CityResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var city = await context.Cities.FirstOrDefaultAsync(x => x.Id == request.id);

            if (city == null)
            {
                throw new NotFoundException(nameof(City),request.id);
            }
                
            context.Remove(city);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<CityResponse>(city);
        }
    }
}
