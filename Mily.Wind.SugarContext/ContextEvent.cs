using Mily.Wind.Extens.Enumeration;
using Mily.Wind.SugarEntity;
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
        public MongoEntity Mongo { get; private set; }
        internal virtual void BeforeExecute<T>(T entity, MongoHandleLogEnum handle) where T: BasicEntity, new()
        {
            Mongo = new MongoEntity
            {
                HandleLog = handle.ToAttr<MongoHandleLogEnum, DescriptionAttribute>(handle.ToString()).Description,
                HandleTime = DateTime.Now,
                TenantId = Caches.RedisCacheGet<MilyUser>("1").TenantId,
                UserId =Caches.RedisCacheGet<MilyUser>("1").Id,
                TargetTable = ((SugarTable)entity.GetType().GetCustomAttributes(typeof(SugarTable), false).FirstOrDefault()).TableName
            };
        }
        internal virtual void AfterExecute()
        {
           Caches.MongoDBCacheSet(Mongo);
        }
    }
}
