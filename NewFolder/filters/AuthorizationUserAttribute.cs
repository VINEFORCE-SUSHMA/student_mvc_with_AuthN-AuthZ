using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace STUDENT.Filters
{
    public class AuthorizeUserAttribute : ActionFilterAttribute
    {
        public string Role { get; set; } // Optional role

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var userId = session.GetInt32("UserId");
            var userRole = session.GetString("Role");

            if (userId == null)
            {
                // Not logged in
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }    

            if (!string.IsNullOrEmpty(Role) && userRole != Role)
            {
                context.Result = new ContentResult
                {
                    Content = "Access Denied. You do not have permission to access this page."
                };
                return;
            }

            base.OnActionExecuting(context);
        }
    }
    }

