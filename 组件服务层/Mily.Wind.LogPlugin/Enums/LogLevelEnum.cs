using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.LogPlugin.Enums
{
    public enum LogLevelEnum
    {
        [Description("调式")]
        Debug,
        [Description("信息")]
        Info,
        [Description("警告")]
        Warning,
        [Description("错误")]
        Error
    }
}
