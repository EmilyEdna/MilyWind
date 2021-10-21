using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.LogPlugin;
using System;
using XExten.Advance.AopFramework.AopAttribute;

namespace Mily.Wind.Extens.AOPUtity
{
    public class ActionsAttribute: AopBaseActionAttribute
    {
        public override void Before(string methodName, object[] parameters)
        {
            //这里可以做权限或者数据库隔离
        }
        public override object After(string methodName, object result)
        {
            MilyLogOption.TraceNode = string.Empty;
            return result;
        }
    }
}
