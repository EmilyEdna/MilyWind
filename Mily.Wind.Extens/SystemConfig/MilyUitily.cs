using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Mily.Wind.VMod.DataTransferObj;
using Mily.Wind.VMod;

namespace Mily.Wind.Extens.SystemConfig
{
    public static class MilyUitily
    {
        public static string ReadFileContent(this string path)
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

        public static void WriteFileContent(this string path, string content)
        {
            if (File.Exists(path))
                File.Delete(path);
            using var fs = new FileStream(path, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            fs.Flush();
        }

        public static T Transfer<T>(this object data) where T : class, IVMCastle, new()
        {
            if (data.GetType() == typeof(T))
                return (T)data;
            else
                return new T { DSCode = DSConst.DS002 };
        }

        public static List<T> Transfers<T>(this object data) where T : class, IVMCastle, new()
        {
            List<T> ls = new List<T>();
            if (data.GetType() == typeof(DefaultVM))
            {
                ls.Add(new T { DSCode = DSConst.DS002 });
                return ls;
            }
            foreach (var item in data as dynamic)
            {
                if (item.GetType() == typeof(T))
                    ls.Add((T)item);
            }
            return ls;
        }
    }
}
