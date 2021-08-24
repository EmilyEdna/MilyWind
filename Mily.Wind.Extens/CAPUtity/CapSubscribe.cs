using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.CAPUtity
{
    public class CapSubscribe : ICapSubscribe
    {
        [CapSubscribe("Test")]
        public void Subscribe(string obj)
        {
            var x = obj;
        }
    }
}
