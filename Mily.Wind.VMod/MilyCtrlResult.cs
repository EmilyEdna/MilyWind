using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod
{
    /// <summary>
    /// 结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MilyCtrlResult<T>
    {
        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// 系统代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Http状态码
        /// </summary>
        public int? HttpCode { get; set; }
        /// <summary>
        /// 创建结果
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static MilyCtrlResult<T> CreateResult(Action<MilyCtrlResult<T>> action)
        {
            MilyCtrlResult<T> ctrl = new MilyCtrlResult<T>();
            action(ctrl);
            return ctrl;
        }
        /// <summary>
        /// 设置错误码
        /// </summary>
        /// <param name="code"></param>
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
                case DSConst.DS003:
                    HttpCode = 999;
                    break;
                default:
                    break;
            }

        }
    }
}
