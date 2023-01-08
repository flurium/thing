using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Thing.Models;

namespace Thing.Filters
{
    public class NotBannedFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.IsInRole(Roles.Banned)) context.Result = new ViewResult { ViewName = "Banned" };
        }
    }
}