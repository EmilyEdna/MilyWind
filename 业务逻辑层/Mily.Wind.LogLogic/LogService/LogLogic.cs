using Mily.Wind.Extens.AOPUtity;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod.Enums;
using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.CacheFramework.MongoDbCache;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.LogLogic.LogService
{
    public class LogLogic : ILogLogic
    {
        public virtual MilyMapperResult GetLogPage(LogInput input)
        {
            var query = MongoDbCaches.Query<ExceptionLog>().AsQueryable();
            if (input.LogLv.HasValue)
                query = query.Where(t => t.LogLv == (LogLevelEnum)input.LogLv.Value);
            if (input.Start.HasValue)
                query = query.Where(t => t.CreatedTime >= input.Start);
            if (input.End.HasValue)
                query = query.Where(t => t.CreatedTime < input.End);
            if (!input.KeyWord.IsNullOrEmpty())
                query = query.Where(t => t.ErrorMsg.Contains(input.KeyWord) || t.StackTrace.Contains(input.KeyWord) || t.TraceId == input.KeyWord);
            var detail = query.Where(t => t.LogEnv == (LogEnvEnum)input.LogEnv)
                .OrderByDescending(t => t.CreatedTime)
                .Skip(input.PageIndex * input.PageSize)
                .Take(input.PageSize).ToList();
            return MilyMapperResult.Success<LogOutput>(new LogOutput
            {
                Detail = detail,
                Total = query.Count()
            });
        }

        public virtual MilyMapperResult DeleteLog(Guid Id)
        {
            var res = MongoDbCaches.Delete<ExceptionLog>(t => t.Id == Id) > 0;
            return MilyMapperResult.DefaultSuccess(res);
        }

        public virtual MilyMapperResult GetSystemService() 
        {
            var res =  MongoDbCaches.Query<ExceptionLog>().AsQueryable().Select(t => t.SystemService).ToList();
            return MilyMapperResult.DefaultSuccess(res);
        }

        public virtual MilyMapperResult WriteLog(List<LogWriteInput> input)
        {
            Caches.MongoDBCacheSet(input.ToMapest<List<ExceptionLog>>());
           return MilyMapperResult.DefaultSuccess(true);
        }
    }
}
