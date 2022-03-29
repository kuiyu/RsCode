/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：lrj
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git

 * 文档 https://rscode.cn/
 */

using System.Threading.Tasks;

namespace RsCode
{
    public class ScheduleJob 
    {
        
        public void Start()
        {
             Task.Run(async () => await CustomJobAsync());
        }

        public virtual  ValueTask CustomJobAsync()
        {
            return new ValueTask();
        }

    }
}
