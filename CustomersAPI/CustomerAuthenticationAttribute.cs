using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomersAPI
{
    public class CustomerAuthenticationAttribute:Attribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var headers = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token=headers?.Split(" ").Last();
            if(token is null)
            {
                context.Result = new JsonResult(
                    new { message = "you are not authorized to use this API" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            else
            {
                var validator = context.HttpContext.RequestServices.GetService<TokenValidator>();
                var user = validator.Validate(token).Result;
                if (user is null) 
                {
                    context.Result = new JsonResult(
                        new { message = "You are not Authorized to use this Api" })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
            }
          
        }
    }
}
