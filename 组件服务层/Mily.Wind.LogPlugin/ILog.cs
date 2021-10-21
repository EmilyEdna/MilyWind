using Mily.Wind.LogPlugin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.LogPlugin
{
    public interface ILog
    {
        void WriteLog(string Message, LogLevelEnum Type= LogLevelEnum.Info, object Param = null, Exception exception=null);
    }
}
