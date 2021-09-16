using Mily.Wind.VMod.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.InternalInterface
{
    public interface ILog
    {
        void WriteInfoLog(string Message, string Invoken, List<object> Param = null);
        Task WriteInfoLogAsync(string Message, string Invoken, List<object> Param = null);
        void WriteWarnLog(string Message, string Invoken, List<object> Param = null);
        Task WriteWarnLogAsync(string Message, string Invoken, List<object> Param = null);
        void WriteErrorLog(string Message, Exception exception, List<object> Param = null);
        Task WriteErrorLogAsync(string Message, Exception exception, List<object> Param = null);
    }
}
