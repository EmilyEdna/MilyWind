using Mily.Wind.VMod.Mogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.AopFramework.AopAttribute;
using XExten.Advance.CacheFramework;
using XExten.Advance.StaticFramework;

namespace Mily.Wind.Extens.AOPUtity
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InterceptorAttribute : InterceptorBaseAttribute
    {
        public override object Invoke(object obj, string methodName, object[] parameters)
        {
            return SyncStatic.TryCatch(() =>
             {
                 return base.Invoke(obj, methodName, parameters);
             }, ex =>
             {
                 var Log = new ExceptionLog
                 {
                     EntityName = obj.GetType().Name,
                     Trace = ex.StackTrace,
                     ErrorMsg = ex.Message,
                     CreatedTime = DateTime.Now,
                     Invoken = methodName,
                     Param = parameters.ToList()
                 };
                 Caches.MongoDBCacheSet(Log) ;
                 //写日志
                 return null;
             });

        }
    }
}
