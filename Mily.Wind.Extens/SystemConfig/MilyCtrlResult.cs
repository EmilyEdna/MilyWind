using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mily.Wind.VMod;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilyCtrlResult<T>
    {
        public T Result { get; set; }
        public string Code { get; set; }
        public int? HttpCode { get; set; }
        public static MilyCtrlResult<T> CreateResult(Action<MilyCtrlResult<T>> action)
        {
            MilyCtrlResult<T> ctrl = new MilyCtrlResult<T>();
            action(ctrl);
            return ctrl;
        }
        public void SetHttpCode(string code)
        {
            switch (code)
            {
                case DSConst.DS001:
                    HttpCode = 200;
                    break;
                case DSConst.DS002:
                    HttpCode = 500;
                    break;
                default:
                    break;
            }

        }
    }
}
