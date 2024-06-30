using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Attributes.TypeAuthorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class TypeAuthorizeAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public MemberType[]? Types { get; set; } = null;

        public TypeAuthorizeAttribute(MemberType[]? types = null)
        {
            Types = types;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (Types != null && Types.Count() > 0)
            {
                var isInType = Types.ToList().FindIndex(t => t.ToString() == context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type").Value);

                if (isInType == -1)
                {
                    context.Result = new ObjectResult("Forbidden") { StatusCode = 403 };
                }
            }
        }
    }
}

