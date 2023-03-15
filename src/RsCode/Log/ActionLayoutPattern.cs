using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Log
{
    internal class ActionLayoutPattern:PatternLayout
    {
        public ActionLayoutPattern()
        {
            this.AddConverter("actionInfo", typeof(ActionConverter));
        }
    }
}
