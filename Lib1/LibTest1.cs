using System;
using System.ComponentModel;

namespace Lib1
{
    [Description("我是测试类")]
    public class LibTest1
    {
        [Description("我是测试方法")]
        public string Test1(string input)
        {
            return input;
        }
    }
}
