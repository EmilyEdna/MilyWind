using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.AopFramework.AopAttribute;

namespace Mily.Wind.Extens.AOPUtity
{
    public class ActionsAttribute: AopBaseActionAttribute
    {
        public override void Before(string methodName, object[] parameters)
        {
            Console.WriteLine(methodName);
            Console.WriteLine(string.Join(",", parameters));
        }
        public override object After(string methodName, object result)
        {
            Console.WriteLine(methodName);
            Console.WriteLine(result);
            return result;
        }
    }
}
