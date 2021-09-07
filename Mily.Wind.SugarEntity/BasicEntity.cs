using Mily.Wind.Extens.SystemConfig;
using SqlSugar;
using System;

namespace Mily.Wind.SugarEntity
{
    public class BasicEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键")]
        public long Id { get; set; }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "逻辑删除")]
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// 删除时间
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "DATETIME", ColumnDescription = "创建时间")]
        public DateTime DeletedAt { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDataType = "DATETIME", ColumnDescription = "创建时间")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        /// <summary>
        /// 所属用户
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDataType = "INT", ColumnDescription = "租户")]
        public int TenantId { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public void DeleteAction()
        {
            this.DeletedAt = DateTime.Now;
            this.IsDeleted = true;
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="SaasId"></param>
        public void CreateAction(int SaasId = 0)
        {
            this.IsDeleted = false;
            this.CreatedAt = DateTime.Now;
            this.Id = MilySnowIdGen.IdGen.CreateId();
            this.TenantId = SaasId;
        }
    }
}
