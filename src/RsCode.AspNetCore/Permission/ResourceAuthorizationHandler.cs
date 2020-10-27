using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.AspNetCore.Permission
{
    public class ResourceAuthorizationHandler : AuthorizationHandler<ResourceRequirement, ResourceRequirementData>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceRequirement requirement, ResourceRequirementData resource)
        {
            //if
            context.Succeed(requirement); 
        }
    }

    public class ResourceRequirement : IAuthorizationRequirement 
    {

    }
}
