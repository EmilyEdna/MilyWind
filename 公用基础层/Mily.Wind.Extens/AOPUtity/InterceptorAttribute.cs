using AutoMapper;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod;
using Mily.Wind.VMod.Enums;
using Mily.Wind.VMod.Mogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.AopFramework.AopAttribute;
using XExten.Advance.CacheFramework;
using XExten.Advance.LinqFramework;
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
                 MilyMapperResult ret = (MilyMapperResult)base.Invoke(obj, methodName, parameters);
                 ret.Result = ret.MapTo == null ? ret.Result : ret.Result.ToMapest(ret.MapTo);
                 var type = ret.Result.GetType();
                 if (type.IsClass && !type.IsGenericType)
                     (ret.Result as IVMCastle).DSCode = ret.Code;
                 if (type.IsClass && type.IsGenericType)
                     foreach (var item in ret.Result as dynamic)
                     {
                         (item as IVMCastle).DSCode = ret.Code;
                     }
                 return ret;
             }, ex =>
             {
                 //var Log = new ExceptionLog
                 //{
                 //    EntityName = obj.GetType().Name,
                 //    StackTrace = ex.InnerException != null ? ex.InnerException.StackTrace : ex.StackTrace,
                 //    ErrorMsg = $"{(ex.InnerException != null ? ex.InnerException.Message : ex.Message)}-请求方法【{methodName}】-请求参数【{parameters.ToJson()}】",
                 //    CreatedTime = DateTime.Now,
                 //    Invoken = methodName,
                 //    Param = parameters.ToJson(),
                 //    LogLv = LogLevelEnum.Error,
                 //    TraceId = ACStatic.AC003,
                 //};
                 //Caches.MongoDBCacheSet(Log);
                 //写日志
                 return MilyMapperResult.DefaultError();
             });

        }
    }
}
