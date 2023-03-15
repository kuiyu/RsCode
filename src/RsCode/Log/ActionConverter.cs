using log4net.Core;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RsCode.Log
{
    internal class ActionConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var actionInfo = loggingEvent.MessageObject as SystemLogInfo;
            if(actionInfo != null)
            {
                switch (this.Option.ToLower())
                { 
                    default:
                        writer.WriteLine("");
                        break;
                }
            }else
            {
                writer.Write("");
            }
        }
    }
}
