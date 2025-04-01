using LearningWebApi.Models.DomainModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningWebApi.Filters
{
    public class ActionFilterForCity : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
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
