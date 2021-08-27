using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptConsole
{
    [SugarTable("smstemplates")]
    public class SmsTemplate
    {
        public long Id { get; set; }

        public DateTime CreationTime { get; set; }

        public long CreatorUserId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public long? LastModifierUserId { get; set; }

        public bool IsDeleted { get; set; }

        public long? DeleterUserId { get; set; }

        public DateTime? DeletionTime { get; set; }

        public int TenantId { get; set; }

        public long Sort { get; set; }

        public string TemplateName { get; set; }

        public string TemplateContent { get; set; }

        public bool IsActive { get; set; }

        public string TemplateCode { get; set; }
    }
}
