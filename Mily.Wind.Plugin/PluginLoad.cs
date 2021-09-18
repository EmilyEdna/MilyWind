using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Plugin
{
    public class PluginLoad : IPluginLoad
    {
        private Dictionary<string, Tuple<string, byte[]>> _itemSource;
        private string _folder = string.Empty;
        private string _directory;
        public PluginLoad()
        {
            _itemSource = new Dictionary<string, Tuple<string, byte[]>>();
            _folder = AppDomain.CurrentDomain.BaseDirectory;
        }
        public void SetDirectory(string directory)
        {
            _directory = directory;
        }
        public void RegistPlugin(Dictionary<string, string> dllInfo)
        {
            if (!string.IsNullOrEmpty(_directory))
                _folder = Path.Combine(_folder, _directory);
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
        public object Excute(string module, string excuteMethod, params object[] param)
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
    }
}
