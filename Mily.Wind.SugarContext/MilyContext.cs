using Microsoft.Extensions.Configuration;
using Mily.Wind.Extens.DependencyInjection;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.SugarEntity;
using Mily.Wind.VMod.Enums;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
                ConnectionString = IocManager.Configuration.GetConnectionString("MySql")
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
            BeforeExecute(entity, HandleLogEnum.Create);
            var ret = Context(migration).Insertable(entity).CallEntityMethod(t => t.CreateAction(0)).ExecuteReturnEntity();
            AfterExecute();
            return ret;
        }

        public bool Alter<T>(T entity, Expression<Func<T, bool>> expression = null, bool migration = false) where T : BasicEntity, new()
        {
            BeforeExecute(entity, HandleLogEnum.Update);
            IUpdateable<T> alter = Context(migration).Updateable(entity);
            bool ret;
            if (expression != null)
                ret = alter.Where(expression).ExecuteCommandHasChange();
            else
                ret = alter.ExecuteCommandHasChange();
            AfterExecute();
            return ret;
        }

        public bool Delete<T>(T entity, Expression<Func<T, bool>> expression, bool migration = false) where T : BasicEntity, new()
        {
            BeforeExecute(entity, HandleLogEnum.Update);
            bool ret = Context(migration).Updateable(entity).CallEntityMethod(t => t.DeleteAction()).Where(expression).ExecuteCommandHasChange();
            AfterExecute();
            return ret;
        }
    }
}
