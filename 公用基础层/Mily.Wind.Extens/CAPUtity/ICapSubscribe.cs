using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.CAPUtity
{
    public interface ICapSubscribe: DotNetCore.CAP.ICapSubscribe
    {
        void Subscribe(string obj);
    }
}
