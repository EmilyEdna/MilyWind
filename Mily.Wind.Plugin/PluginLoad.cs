using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Plugin
{
    public class PluginLoad
    {
        private static Dictionary<string, Tuple<string, byte[]>> _itemSource;
        private static string _folder = string.Empty;
        private static string _directory = "plugin";
        static PluginLoad()
        {
            _itemSource = new Dictionary<string, Tuple<string, byte[]>>();
            _folder = AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <param name="dllInfo">键值是DLL的名称，值是调用这个DLL启动类的名称</param>
        /// <param name="directory"></param>
        public static void RegistPlugin(Dictionary<string, string> dllInfo)
        {
            _folder = Path.Combine(_folder, _directory);
            if (!Directory.Exists(_folder))
                Directory.CreateDirectory(_folder);
            foreach (var item in dllInfo)
            {
                string fn = item.Key;
                if (!item.Key.Contains(".dll"))
                    fn = $"{item.Key}.dll";
                using (FileStream fs = new FileStream(Path.Combine(_folder, fn), FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Seek(0, SeekOrigin.Begin);
                    _itemSource.TryAdd(item.Key, new Tuple<string, byte[]>(item.Value, buffer));
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
        /// <summary>
        /// 调用插件
        /// </summary>
        /// <param name="module">需要被执行的类，必须同注册插件中的启动类是同一个</param>
        /// <param name="excuteMethod">需要执行的方法</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object Excute(string module, string excuteMethod, params object[] param)
        {
            var state = _itemSource.TryGetValue(module, out Tuple<string, byte[]> ass);
            if (state == false) return null;
            using (var stream = new MemoryStream(ass.Item2))
            {
                PluginLoadContext context = new PluginLoadContext();
                var assembly = context.LoadFromStream(stream);
                var type = assembly.GetTypes().FirstOrDefault(t => t.Name.Equals(ass.Item1));
                var result = type.GetMethod(excuteMethod).Invoke(Activator.CreateInstance(type), param);
                context.Unload();
                return result;
            }
        }

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] RegistPlugin(IFormFile file)
        {
            if (!Directory.Exists(PluginConfig.PluginRoute))
            {
                Directory.CreateDirectory(PluginConfig.PluginRoute);
            }
            var filePath = Path.Combine(PluginConfig.PluginRoute, file.FileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            using FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite);
            file.CopyTo(fs);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Seek(0, SeekOrigin.Begin);
            fs.Flush();
            return buffer;
        }


    }
}
