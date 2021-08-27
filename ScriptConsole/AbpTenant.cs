using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptConsole
{
    [SugarTable("abptenants")]
    public class AbpTenant
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string TenancyName { get; set; }
        public string CompanyName { get; set; }
    }
}
