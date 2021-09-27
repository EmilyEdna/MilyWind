using System;
using System.ComponentModel;

namespace Lib1
{
    [Description("我是测试类")]
    public class LibTest1
    {
        [Description("我是测试方法1")]
        public string Test1(string input)
        {
            return input;
        }
        [Description("我是测试方法2")]
        public int Test2(int input)
        {
            return input * 10;
        }
    }
}
