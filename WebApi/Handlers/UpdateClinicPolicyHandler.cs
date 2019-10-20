using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Handlers
{
    public class UpdateClinicPolicyHandler : AuthorizationHandler<UpdateClinicPolicyRequirement>
    {
        readonly IHttpContextAccessor _contextAccessor;

        public UpdateClinicPolicyHandler(IHttpContextAccessor ca)
        {
            _contextAccessor = ca;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UpdateClinicPolicyRequirement requirement)
        {
            if (context.Resource is AuthorizationFilterContext filterContext)
            {

                if (await requirement.Pass(_contextAccessor))
                    context.Succeed(requirement);
                else
                    context.Fail();

            }
            else if (context.Resource is PolicyResource policyResource)
            {
                var pr = policyResource;
                if (await requirement.Pass(_contextAccessor))
                    context.Succeed(requirement);
                else
                    context.Fail();
            }
            else
            {
                context.Fail();
            }
        }
    }
}
