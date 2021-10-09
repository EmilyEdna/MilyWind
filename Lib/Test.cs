using System;

namespace Lib
{
    public class Test : IPlugin
    {
        public string Name => "我是测试插件";

        public string Description => "我是测试插件";

        public string Execute(object inPars)
        {
            return "我是测试插件";
        }
    }
}
