using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dal.Filters
{
    public class NotBannedFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.IsInRole(Domain.Models.Roles.Banned)) context.Result = new ViewResult { ViewName = "Banned" };
        }
    }
}