using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.SugarEntity.System
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    [SugarTable("Sys_MilyUser", "用户信息表")]
    public class MilyUser : BasicEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnDescription = "用户名", ColumnDataType = "VARCHAR", Length = 50)]
        public string Name { get; set; }
        /// <summary>
        /// 明文密码
        /// </summary>
        [SugarColumn(ColumnDescription = "明文密码", ColumnDataType = "VARCHAR", Length = 50)]
        public string Password { get; set; }
        /// <summary>
        /// 加密密码
        /// </summary>
        [SugarColumn(ColumnDescription = "加密密码", ColumnDataType = "VARCHAR", Length = 50)]
        public string EncryptPassword { get; set; }

        public MilyUser SetEncryptPassword()
        {
            this.EncryptPassword = this.Password.ToMd5();
            return this;
        }
    }
}
