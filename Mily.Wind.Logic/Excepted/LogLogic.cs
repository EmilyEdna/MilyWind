using Mily.Wind.Extens.AOPUtity;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod.Enums;
using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Linq;
using XExten.Advance.CacheFramework.MongoDbCache;

namespace Mily.Wind.Logic.Excepted
{
    [Interceptor]
    public class LogLogic : ILogLogic
    {
        [Actions]
        public virtual MilyMapperResult GetLogPage(LogInput input)
        {
            var query = MongoDbCaches.Query<ExceptionLog>().AsQueryable();
            if (input.LogLv.HasValue)
                query = query.Where(t => t.LogLv == (LogLevelEnum)input.LogLv.Value);
            if (input.Start.HasValue)
                query = query.Where(t => t.CreatedTime >= input.Start);
            if (input.End.HasValue)
                query = query.Where(t => t.CreatedTime < input.End);
            var detail = query.OrderByDescending(t=>t.CreatedTime).Skip(input.PageIndex * input.PageSize).Take(input.PageSize).ToList();
            return MilyMapperResult.Success<LogOutput>(MapperEnum.Class, new LogOutput
            {
                Detail = detail,
                Total = query.Count()
            });
        }
        [Actions]
        public virtual MilyMapperResult DeleteLog(Guid Id)
        {
            var res = MongoDbCaches.Delete<ExceptionLog>(t => t.Id == Id) > 0;
            return MilyMapperResult.DefaultSuccess(res);
        }
    }
}
