using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.Storage.QiniuStorage.Core
{
    public interface IZoneHelper
    {
        Task<Zone> QueryZoneAsync(string bucket);
    }
}
