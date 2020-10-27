using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace RsCode.AspNetCore.Permission
{
    public class ResourceRequirementFilter : IAsyncAuthorizationFilter
    { 

        readonly ResourceRequirementData data;     
        public ResourceRequirementFilter(ResourceRequirementData _data)
        {
            data = _data;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            /*
             * 多个RsAuthorize之间是并且的关系
             * RsAuthorize参数之间是或者的关系
             * 逻辑:
             * 1.得到当前登录用户
             * 2.如果按角色查,查询登录用户角色,查询用户角色是否在指定的角色里
             * 3.如果按分组查,查询登录用户所在的组,查询用户所在的组是否包含在指定组里
             * 4.如果按特定用户查,查询登录用户,是否在指定的用户里
             * 5.如果都不符合,按无权限处理
             * 默认使用策略名为RsPolicy
             */

            //当前登录用户 (组,角色,允许访问的用户)
            var User = context.HttpContext.User; 
            //当前指定的资源


            //用户的角色 
            var userRoles = new string[] { };
            //用户所在的组 
            var userGroups =new string[] { };
            if (Validate(AllowRole, userRoles, data.ResourceName)
                || Validate(AllowGroup, userGroups, data.ResourceName))
            {

            }
            else if (User.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
            }
            else
            {
                context.Result = new ChallengeResult();
            }




            //    //根据资源名称查询资源(包含了该资源可允许访问的用户)
            //    ResourceInfo doc = new ResourceInfo();

            ////查询资源允许访问的
            //if (!string.IsNullOrWhiteSpace(data.Policy))
            //{
            //    var r = await authorizationService.AuthorizeAsync(User, data, data.Policy);
            //}


        }





        public bool Validate(Func<string[],string,bool> func,string[] tag,string ResourceName)
        {
            return func(tag,ResourceName);
        }

        /// <summary>
        /// 查询用户所在组信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string[] GetUserGroups(long UserId)
        {
            return null;
        }
        /// <summary>
        /// 查询用户角色信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string[]GetUserRoles(long UserId)
        {
            return null;
        }

        /// <summary>
        /// 指定角色是否有权访问
        /// </summary>
        /// <param name="Roles">用户的角色</param>
        /// <param name="ResourceName"></param>
        /// <returns></returns>
        public bool AllowRole(string[] Roles,string ResourceName)
        {
            //用户角色是否在指定的访问的角色里
            //该角色是否对资源有访问权限
            return true;
        }

        /// <summary>
        /// 指定组是否有权访问
        /// </summary>
        /// <param name="Groups"></param>
        /// <param name="ResourceName"></param>
        /// <returns></returns>
        public bool AllowGroup(string[]Groups,string ResourceName)
        {
            //用户所在的组是否在在指定的组里
            //该组是否对资源有访问权限
            return true;
        }
    }
}
