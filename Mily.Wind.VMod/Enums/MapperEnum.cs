using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Enums
{
    public enum MapperEnum
    {
        [Description("空")]
        None,
        [Description("创建")]
        Collection,
        [Description("创建")]
        Class,
        [Description("默认成功")]
        DefaultSuccess
    }
}
