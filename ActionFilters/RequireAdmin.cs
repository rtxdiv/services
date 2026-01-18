using Microsoft.AspNetCore.Mvc.Filters;

namespace services.ActionFilters
{
    public class RequireAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("RequireAdminAttribute");
        }
    }
}