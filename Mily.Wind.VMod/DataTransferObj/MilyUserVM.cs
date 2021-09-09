using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.DataTransferObj
{
    public class MilyUserVM : IVMCastle
    {
        /// <summary>
        /// PK
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 系统代码
        /// </summary>
        public string DSCode { get; set; }
    }
}
