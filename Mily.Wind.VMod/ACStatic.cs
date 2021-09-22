using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.StaticFramework;

namespace Mily.Wind.VMod
{
    public class ACStatic
    {
        /// <summary>
        /// API
        /// </summary>
        public static Dictionary<string, string> AC001
        {
            get
            {
                var Info = new Dictionary<string, string>();
                Info.Add(DSConst.Logics, "业务");
                Info.Add(DSConst.Logs, "日志");
                Info.Add(DSConst.Plugins, "插件");
                return Info;
            }
        }

        /// <summary>
        /// XML描述
        /// </summary>
        public static List<string> AC002
        {
            get
            {
                List<string> xml = new List<string>();
                SyncStatic.Assembly("Mily.Wind").ForEach(item =>
                {
                    var xmlName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{item.GetName().Name}.xml");
                    if (File.Exists(xmlName))
                    {
                        xml.Add(xmlName);
                    }
                });
                return xml;
            }
        }

        /// <summary>
        /// 异常识别码
        /// </summary>
        public static string AC003 { get; set; }

    }
}