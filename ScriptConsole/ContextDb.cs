using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptConsole
{
    public class ContextDb
    {
        public SqlSugarClient Context(bool migration = false)
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig
            {
                IsAutoCloseConnection = true,
                DbType = DbType.MySql,
                ConnectionString = "Server=rm-8vbc2h2n4o772g40yxo.mysql.zhangbei.rds.aliyuncs.com; Database=yls_shop_test; User=releaseu_test;Password=eI5H7xGpEmIE;charset=utf8"
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
            return db;
        }
    }
}
