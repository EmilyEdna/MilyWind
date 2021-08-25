using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.AopFramework.AopAttribute;
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
                 //写日志
                 return null;
             });

        }
    }
}
