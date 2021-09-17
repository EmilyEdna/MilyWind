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
            Policy.Handle<Exception>().Retry(Times, (Ex, Count, Context) =>
            {
                Console.WriteLine($"重试次数：{Count}，异常信息：{Ex.Message}，错误方法：{Context["MethodName"]}");
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
            return Policy.Handle<Exception>().Retry(Times, (Ex, Count, Context) =>
            {
                Console.WriteLine($"重试次数：{Count}，异常信息：{Ex.Message}，错误方法：{Context["MethodName"]}");
            }).Execute(action);
        }
        /// <summary>
        /// 短路由无返回
        /// </summary>
        /// <param name="action"></param>
        /// <param name="Times"></param>
        /// <param name="Seconds"></param>
        public static void DoRetry(Action action, int Times = 3, int Seconds = 60)
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
        public static T DoRetry<T>(Func<T> action, int Times = 3, int Seconds = 60)
        {
            return Policy.Handle<Exception>().CircuitBreaker(Times, TimeSpan.FromSeconds(Seconds)).Execute(action);
        }
    }
}
