using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 从uc.qbox.me返回的消息
    /// </summary>
    internal class ZoneInfo
    {
        public int Ttl { get; set; }
        public Io Io { set; get; }
        public Up Up { set; get; }

        public Rs Rs { get; set; }

        public Rsf Rsf { get; set; }

        public Api Api { get; set; }
    }

    internal class Io
    {
        public Src Src { set; get; }
    }

    internal class Src
    {
        public string[] Main { set; get; }
    }

    internal class Up
    {
        public UpDomain Acc { set; get; }
        public UpDomain OldAcc { set; get; }
        public UpDomain Src { set; get; }
        public UpDomain OldSrc { set; get; }
    }

    internal class Rs
    {
        public UpDomain Acc { get; set; }
    }

    internal class Rsf
    {
        public UpDomain Acc { get; set; }
    }

    internal class Api
    {
        public UpDomain Acc { get; set; }
    }
    internal class UpDomain
    {
        public string[] Main { set; get; }
        public string[] Backup { set; get; }
        public string Info { set; get; }
    }
     
}
