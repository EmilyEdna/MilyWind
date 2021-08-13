using SqlSugar;
using System;

namespace Mily.Wind.SugarEntity
{
    public class BasicEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键", IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "逻辑删除")]
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDataType = "DATETIME", ColumnDescription = "创建时间")]
        public DateTime Created { get; set; } = DateTime.Now;
        /// <summary>
        /// 所属用户
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDataType = "INT", ColumnDescription = "租户")]
        public int TenantId { get; set; }
    }
}
