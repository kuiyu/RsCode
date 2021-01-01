using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RsCode.Storage.QiniuStorage.Core
{
    /// <summary>
    /// 存储区域
    /// </summary>
    public enum Region
    {
        /// <summary>
        /// 华东
        /// </summary>
        [Description("z0")]
        z0,
        /// <summary>
        /// 华北
        /// </summary>
        [Description("z1")]
        z1,
        /// <summary>
        /// 华南
        /// </summary>
        [Description("z2")]
        z2,
        /// <summary>
        /// 北美
        /// </summary>
        [Description("na0")]
        na0,
        /// <summary>
        /// 东南亚
        /// </summary>
        [Description("as0")]
        as0
    }
}
