using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.DependencyInjection
{
    /// <summary>
    /// 限制特性只能标记在属性上(该特性什么都不做，只用来标记属性注入)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyInjectionAttribute : Attribute
    {

    }
}
