using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Input
{
    public class PluginExcuterAlterInput
    {
        /// <summary>
        /// 键Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 执行器键值
        /// </summary>
        public string ExcuteKey { get; set; }
    }
}
