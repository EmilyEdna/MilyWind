using Mily.Wind.Extens.AOPUtity;
using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;

namespace Mily.Wind.Logic.Excepted
{
    [Interceptor]
    public class LogLogic : ILogLogic
    {
        [Actions]
        public virtual LogOutput GetLogPage(LogInput input)
        {
            var data = Caches.MongoDBCachesGet<ExceptionLog>(t => t.EntityName != null).ToList();
            var detail = data.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList();
            return new LogOutput
            {
                Detail = detail,
                Total = data.Count
            };
        }
    }
}
