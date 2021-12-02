using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.LogPlugin;
using System;
using XExten.Advance.AopFramework.AopAttribute;

namespace Mily.Wind.Extens.AOPUtity
{
    public class ActionsAttribute : AopBaseActionAttribute
    {
        public ActionsAttribute() : base() { }


        public ActionsAttribute(string Code) : base(Code) { }

        public override void Before(string methodName, Type classInfo, object[] parameters)
        {
            //这里可以做权限或者数据库隔离
        }
        public override object After(string methodName, Type classInfo, object result)
        {
            MilyLogOption.TraceNode = string.Empty;
            return result;
        }
    }
}
