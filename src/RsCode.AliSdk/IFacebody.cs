using AlibabaCloud.SDK.Facebody20191230.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.AliSdk
{
    public interface IFacebody
    {
        /// <summary>
        /// 人物动漫化
        /// 
        /// <see cref="https://next.api.aliyun.com/api/facebody/2019-12-30/GenerateHumanAnimeStyle?accounttraceid=ed05de837a77468682bfa4255418b19eaunz&lang=CSHARP&params={}"/>
        /// </summary>
        /// <param name="generateHumanAnimeStyleRequest">
        /// </param>
        /// <returns></returns>
        Task<GenerateHumanAnimeStyleResponse> GenerateHumanAnimeStyleAsync(GenerateHumanAnimeStyleAdvanceRequest generateHumanAnimeStyleRequest);
    }
}
