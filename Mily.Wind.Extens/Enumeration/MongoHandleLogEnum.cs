using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.Enumeration
{
    public enum MongoHandleLogEnum
    {
        [Description("创建")]
        Create,
        [Description("删除")]
        Delete,
        [Description("更新")]
        Update
    }
}
