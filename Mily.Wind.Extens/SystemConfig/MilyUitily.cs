using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilyUitily
    {
        public static string ReadFileContent(string path)
        {
            if (!File.Exists(path)) return "No File Exist";
            using var fs = File.OpenRead(path);
            byte[] bytes = new byte[1024];
            StringBuilder sb = new StringBuilder();
            while (fs.Read(bytes, 0, bytes.Length) > 0)
            {
                sb.Append(Encoding.UTF8.GetString(bytes));
            }
            fs.Close();
            fs.Flush();
            return sb.ToString();
        }

        public static void WriteFileContent(string path,string content)
        {
            if (File.Exists(path)) 
                File.Delete(path);
            using var fs = new FileStream(path, FileMode.Create);
            byte[] bytes =  Encoding.UTF8.GetBytes(content);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            fs.Flush();
        }
    }
}
