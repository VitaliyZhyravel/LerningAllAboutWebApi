using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningWebApi.Filters
{
    public class ExeptionFilterForCityController : IExceptionFilter
    {
        private readonly IHostEnvironment environment;

        public ExeptionFilterForCityController(IHostEnvironment environment)
        {
            this.environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            if (environment.IsDevelopment()) 
            {
                var response = new
                {
                    error = "Внутрішня помилка сервера",
                    details = context.Exception.InnerException?.Message
                };
                context.Result = new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
