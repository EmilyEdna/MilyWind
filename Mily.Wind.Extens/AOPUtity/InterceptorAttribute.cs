using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.AopFramework.AopAttribute;

namespace Mily.Wind.Extens.AOPUtity
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InterceptorAttribute: InterceptorBaseAttribute
    {
        public override object Invoke(object obj, string methodName, object[] parameters)
        {
            return base.Invoke(obj, methodName, parameters);
        }
    }
}
