using Mily.Wind.Extens.Enumeration;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.SugarEntity;
using Mily.Wind.SugarEntity.Mogo;
using Mily.Wind.SugarEntity.System;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.SugarContext
{
    public abstract class ContextEvent
    {
        public HandleLog Mongo { get; private set; }
        internal virtual void PreExecute<T>(T entity) where T : BasicEntity, new()
        {
            entity.Id = MilySnowIdGen.CreateGenId();
            entity.IsDeleted = false;
            entity.Created = DateTime.Now;
            entity.TenantId = 0;
        }
        internal virtual void BeforeExecute<T>(T entity, MongoHandleLogEnum handle) where T: BasicEntity, new()
        {
            Mongo = new HandleLog
            {
                HandleLogs = handle.ToAttr<MongoHandleLogEnum, DescriptionAttribute>(handle.ToString()).Description,
                HandleTime = DateTime.Now,
                TenantId = MilySession.GetSession<MilyUser>()?.TenantId,
                UserId = MilySession.GetSession<MilyUser>()?.Id,
                TargetTable = ((SugarTable)entity.GetType().GetCustomAttributes(typeof(SugarTable), false).FirstOrDefault()).TableName
            };
        }
        internal virtual void AfterExecute()
        {
           Caches.MongoDBCacheSet(Mongo);
        }
    }
}
