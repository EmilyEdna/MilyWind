using Mily.Wind.Extens.AOPUtity;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod.Enums;
using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System.Linq;
using XExten.Advance.CacheFramework.MongoDbCache;

namespace Mily.Wind.Logic.Excepted
{
    [Interceptor]
    public class LogLogic : ILogLogic
    {
        public ILog LogClient => IocManager.GetService<ILog>();

        [Actions]
        public virtual MilyResult GetLogPage(LogInput input)
        {
            var query = MongoDbCaches.Query<ExceptionLog>().AsQueryable();
            if (input.Start.HasValue && input.End.HasValue)
                query = query.Where(t => t.CreatedTime >= input.Start && t.CreatedTime < input.End);
            var detail = query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList();
            return MilyResult.Success<LogOutput, LogOutput>(MapperEnum.Class, new LogOutput
            {
                Detail = detail,
                Total = query.Count()
            });

        }
    }
}
