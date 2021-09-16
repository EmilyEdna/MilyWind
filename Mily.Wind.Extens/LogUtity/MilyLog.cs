using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.VMod;
using Mily.Wind.VMod.Enums;
using Mily.Wind.VMod.Mogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.Extens.LogUtity
{
    public class MilyLog : ILog
    {
        public void WriteInfoLog(string Message, string Invoken, List<object> Param = null)
        {
            var Log = new ExceptionLog
            {
                ErrorMsg = Message,
                CreatedTime = DateTime.Now,
                Invoken = Invoken,
                Param = Param.ToJson(),
                LogLv = LogLevelEnum.Info,
                TraceId = ACStatic.AC003
            };
            Caches.MongoDBCacheSet(Log);
        }
        public async Task WriteInfoLogAsync(string Message, string Invoken, List<object> Param = null)
        {
            var Log = new ExceptionLog
            {
                ErrorMsg = Message,
                CreatedTime = DateTime.Now,
                Invoken = Invoken,
                Param = Param.ToJson(),
                LogLv = LogLevelEnum.Info,
                TraceId = ACStatic.AC003
            };
            await Caches.MongoDBCacheSetAsync(Log);
        }
        public void WriteWarnLog(string Message, string Invoken, List<object> Param = null)
        {
            var Log = new ExceptionLog
            {
                ErrorMsg = Message,
                CreatedTime = DateTime.Now,
                Invoken = Invoken,
                Param = Param.ToJson(),
                LogLv = LogLevelEnum.Warning,
                TraceId = ACStatic.AC003
            };
            Caches.MongoDBCacheSet(Log);
        }
        public async Task WriteWarnLogAsync(string Message, string Invoken, List<object> Param = null)
        {
            var Log = new ExceptionLog
            {
                ErrorMsg = Message,
                CreatedTime = DateTime.Now,
                Invoken = Invoken,
                Param = Param.ToJson(),
                LogLv = LogLevelEnum.Warning,
                TraceId = ACStatic.AC003
            };
            await Caches.MongoDBCacheSetAsync(Log);
        }
        public void WriteErrorLog(string Message, Exception exception,  List<object> Param = null)
        {
            var Log = new ExceptionLog
            {
                ErrorMsg = Message,
                CreatedTime = DateTime.Now,
                StackTrace= exception.InnerException!=null?exception.InnerException.StackTrace:exception.StackTrace,
                Param = Param.ToJson(),
                LogLv = LogLevelEnum.Error,
                TraceId= ACStatic.AC003
            };
            Caches.MongoDBCacheSet(Log);
        }
        public async Task WriteErrorLogAsync(string Message, Exception exception, List<object> Param = null)
        {
            var Log = new ExceptionLog
            {
                ErrorMsg = Message,
                CreatedTime = DateTime.Now,
                StackTrace = exception.InnerException != null ? exception.InnerException.StackTrace : exception.StackTrace,
                Param = Param.ToJson(),
                TraceId = ACStatic.AC003,
                LogLv = LogLevelEnum.Error
            };
            await Caches.MongoDBCacheSetAsync(Log);
        }
    }
}
