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
                DbType = DbType.Sqlite,
                ConnectionString = $"DataSource={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataBase", "Alm.db")}"
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

        public T Insert<T>(T entity) where T : BasicEntity, new()
        {
            BeforeExecute(entity, Extens.Enumeration.MongoHandleLogEnum.Create);
            var ret = Context().Insertable(entity).ExecuteReturnEntity();
            AfterExecute();
            return ret;
        }
    }
}
