using Mily.Wind.LogPlugin;
using Mily.Wind.LogPlugin.Enums;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Extens.RetryUtity
{
    public class Retry
    {
        /// <summary>
        ///  无返回重试
        /// </summary>
        /// <param name="Times"></param>
        /// <returns></returns>
        public static void DoRetry(Action action, int Times = 3)
        {
            ILog Log = new Log();
            Policy.Handle<Exception>().Retry(Times, (Ex, Count, Context) =>
            {
                if (Count == 1)
                    Log.WriteLog(Ex.Message,LogLevelEnum.Error,exception:Ex);
            }).Execute(action);
        }
        /// <summary>
        /// 有返回重试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="Times"></param>
        /// <returns></returns>
        public static T DoRetry<T>(Func<T> action, int Times = 3)
        {
            ILog Log = new Log();
            return Policy.Handle<Exception>().Retry(Times, (Ex, Count, Context) =>
            {
                if (Count == 1)
                    Log.WriteLog(Ex.Message, LogLevelEnum.Error, exception: Ex);
            }).Execute(action);
        }
        /// <summary>
        /// 短路由无返回
        /// </summary>
        /// <param name="action"></param>
        /// <param name="Times"></param>
        /// <param name="Seconds"></param>
        public static void DoRetryBreak(Action action, int Times = 3, int Seconds = 60)
        {
            Policy.Handle<Exception>().CircuitBreaker(Times, TimeSpan.FromSeconds(Seconds)).Execute(action);
        }
        /// <summary>
        /// 短路由有返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="Times"></param>
        /// <param name="Seconds"></param>
        /// <returns></returns>
        public static T DoRetryBreak<T>(Func<T> action, int Times = 3, int Seconds = 60)
        {
            return Policy.Handle<Exception>().CircuitBreaker(Times, TimeSpan.FromSeconds(Seconds)).Execute(action);
        }
    }
}
