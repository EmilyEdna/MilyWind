using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.VMod.Enums;
using Mily.Wind.VMod.Mogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;

namespace Mily.Wind.Extens.LogUtity
{
    public class MilyLog : ILog
    {
        public void WriteLog(string Message, string Invoken, List<object> Param = null, LogLevelEnum Lv = LogLevelEnum.Info)
        {
            var Log = new ExceptionLog
            {
                ErrorMsg = Message,
                CreatedTime = DateTime.Now,
                Invoken = Invoken,
                Param = Param,
                LogLv = Lv
            };
            Caches.MongoDBCacheSet(Log);
        }
        public async Task WriteLogAsync(string Message, string Invoken, List<object> Param = null, LogLevelEnum Lv = LogLevelEnum.Info)
        {
            var Log = new ExceptionLog
            {
                ErrorMsg = Message,
                CreatedTime = DateTime.Now,
                Invoken = Invoken,
                Param = Param,
                LogLv = Lv
            };
            await Caches.MongoDBCacheSetAsync(Log);
        }
    }
}
