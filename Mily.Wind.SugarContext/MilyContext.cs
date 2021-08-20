using Microsoft.Extensions.Configuration;
using Mily.Wind.Extens.Enumeration;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.SugarEntity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.StaticFramework;

namespace Mily.Wind.SugarContext
{
    public class MilyContext : ContextEvent
    {
        public SqlSugarClient Context(bool migration = false)
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig
            {
                IsAutoCloseConnection = true,
                DbType = DbType.MySql,
                ConnectionString = MilyUtily.Configuration.GetConnectionString("MySql")
            });
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //sql 执行前
            };
            db.Aop.OnLogExecuted = (sql, pars) =>
            {
                //sql 执行后
                Console.WriteLine(db.Ado.SqlExecutionTime.ToString());
            };
            db.Aop.OnError = ex =>
            {
                //sql 异常
            };
            if (migration)
            {
                var models = SyncStatic.Assembly("Mily.Wind.SugarEntity")
                        .SelectMany(t => t.ExportedTypes.Where(x => x.BaseType == typeof(BasicEntity))).ToArray();
                db.CodeFirst.InitTables(models);
            }
            return db;
        }

        public T Insert<T>(T entity, bool migration = false) where T : BasicEntity, new()
        {
            PreExecute(entity);
            BeforeExecute(entity, MongoHandleLogEnum.Create);
            var ret = Context(migration).Insertable(entity).ExecuteReturnEntity();
            AfterExecute();
            return ret;
        }
    }
}
