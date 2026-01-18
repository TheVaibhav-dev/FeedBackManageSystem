using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FeedBackManageSystem.Enum;

namespace FeedBackManageSystem.HelperClasses.Filters
{
    public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly UserType[] _allowedRoles;
        public RoleAuthorizeAttribute(params UserType[] roles)
        {
            _allowedRoles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(ProjectSession.UserId == null || ProjectSession.UserType == null)
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
                return;
            }
            if(!_allowedRoles.Contains(ProjectSession.UserType.Value))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
