using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningWebApi.Filters
{
    public class ExeptionFilterForCityController : IExceptionFilter
    {
        private readonly IHostEnvironment _environment;
        private readonly ILogger<ExeptionFilterForCityController> _logger;

        public ExeptionFilterForCityController(IHostEnvironment environment,ILogger<ExeptionFilterForCityController> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogInformation("End {NameFilter}", nameof(ExeptionFilterForCityController));

            if (_environment.IsDevelopment()) 
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
