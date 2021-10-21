using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.LogPlugin;
using Mily.Wind.LogPlugin.Enums;
using Mily.Wind.VMod;
using System;
using XExten.Advance.AopFramework.AopAttribute;
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
                 var LogClient = IocManager.GetService<ILog>();
                 var Msg = $"{ (ex.InnerException != null ? ex.InnerException.Message : ex.Message)}-请求方法【{ methodName}】";
                 var Ex = ex.InnerException != null ? ex.InnerException : ex;
                 LogClient.WriteLog(Msg, LogLevelEnum.Error, parameters, Ex);
                 //写日志
                 return MilyMapperResult.DefaultError();
             });

        }
    }
}
