using LearningWebApi.Models.DomainModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningWebApi.Filters
{
    public class ActionFilterForCity : IActionFilter
    {
        private readonly ILogger<Action> _logger;

        public ActionFilterForCity(ILogger<Action> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("End {NameFilter}",nameof(ActionFilterForCity));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Start {NameFilter}", nameof(ActionFilterForCity));
            if (context.ActionArguments.ContainsKey("FilterOn"))
            {
                string? item = Convert.ToString(context.ActionArguments["FilterOn"]);
                
                if (!item.Equals(nameof(City.Name), StringComparison.OrdinalIgnoreCase))
                {
                    context.ActionArguments["FilterOn"] = nameof(City.Name);
                }
            }
        }
    }
}
