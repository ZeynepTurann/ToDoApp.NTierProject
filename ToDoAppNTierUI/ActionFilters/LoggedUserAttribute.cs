using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoAppNTierUI.ActionFilters
{
    public class LoggedUserAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userId = context.HttpContext.Session.GetString("userId");

            //not login
            if (string.IsNullOrEmpty(userId))
            {
                
                string routePath = context.HttpContext.Request.Path;
                context.Result = new RedirectToActionResult("Login", "User", new { path= routePath });
            }
        }
    }
}
