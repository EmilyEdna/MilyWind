using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.OptionPlugin.Settings
{
    public enum EnvEnum
    {
        [Description("开发")]
        Dev,
        [Description("正式")]
        Pro,
    }
}
