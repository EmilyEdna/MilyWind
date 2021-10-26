using Mily.Wind.VMod.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Input
{
    public class OptionConfInput
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string NameSpace { get; set; }
        /// <summary>
        /// 配置项
        /// </summary>
        public string OptionJson { get; set; }
        /// <summary>
        /// 环境
        /// </summary>
        public EnvEnum Env { get; set; }
    }
}
