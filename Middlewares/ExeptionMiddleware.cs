using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LearningWebApi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExeptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionMiddleware> _logger;

        public ExeptionMiddleware(RequestDelegate next,ILogger<ExeptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
              await _next(httpContext);
            }
            catch (Exception ex)
            {
                
                if (ex.InnerException != null)
                {
                    _logger.LogError("{ExeptionType},{ExeptionMessage}", 
                        ex.InnerException.GetType().ToString(),ex.InnerException.Message);
                }
                else
                {
                    _logger.LogError("{ExeptionType},{ExeptionMessage}",
                       ex.GetType().ToString(), ex.Message);
                }
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync("Server Error");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExeptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExeptionMiddleware>();
        }
    }
}
