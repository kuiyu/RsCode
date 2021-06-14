using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.AspNetCore.Permission
{
    public class ResourceAuthorizationHandler : AuthorizationHandler<ResourceRequirement, ResourceRequirementData>
    {
        protected override  Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceRequirement requirement, ResourceRequirementData resource)
        {
            //if
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class ResourceRequirement : IAuthorizationRequirement 
    {

    }
}
