using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.SugarEntity;
using Mily.Wind.SugarEntity.System;
using Mily.Wind.VMod.Enums;
using Mily.Wind.VMod.Mogo;
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
        public HandlMogoViewModel Mongo { get; private set; }
        internal virtual void BeforeExecute<T>(T entity, HandlLogEnum handle) where T: BasicEntity, new()
        {
            Mongo = new HandlMogoViewModel
            {
                HandleLogs = handle.ToAttr<HandlLogEnum, DescriptionAttribute>(handle.ToString()).Description,
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
