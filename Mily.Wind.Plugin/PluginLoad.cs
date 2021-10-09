using Microsoft.AspNetCore.Http;
using Mily.Wind.Plugin.Infos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.CacheFramework.MongoDbCache;
using XExten.Advance.LinqFramework;
using XExten.Advance.StaticFramework;

namespace Mily.Wind.Plugin
{
    public class PluginLoad
    {

        public static object Excute(string dllName, object inPars)
        {
            var result = Excute(dllName, inPars, out WeakReference reference);
            //检查
            for (int i = 0; reference.IsAlive && (i < 10); i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        /// <summary>
        /// 调用插件
        /// </summary>
        /// <param name="dllName"></param>
        /// <param name="inPars"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected static object Excute(string dllName, object inPars, out WeakReference reference)
        {
            object OjbResult = null;

            PluginInfo Info = Caches.MongoDBCacheGet<PluginInfo>(t => t.PluginName.ToLower() == $"{dllName}.dll".ToLower() && t.IsEable == true);
            var path = Path.Combine(Info.PluginPath, Info.PluginRoute, Info.PluginName);
            PluginLoadContext context = new PluginLoadContext(path);
            //创建一个对AssemblyLoadContext的弱引用，允许我们检测卸载何时完成
            reference = new WeakReference(context);

            var assembly = context.LoadFromAssemblyPath(path).GetTypes().Where(t => t.GetInterface(nameof(IPluginContext)) != null).ToList();

            //创建插件对象并调用
            foreach (var item in assembly)
            {
                var obj = Activator.CreateInstance(item);
                OjbResult = obj.GetType().InvokeMember("Execute", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance, null, obj, new object[] { inPars });
                break;
            }

            context.Unload();
            return OjbResult;
        }
        /// <summary>
        /// 注册插件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static List<PluginInfo> RegistPlugin(IFormFile file)
        {
            return SyncStatic.TryCatch(() =>
             {
                 var fileDir = Path.Combine(PluginConfig.PluginRoute, Path.GetFileNameWithoutExtension(file.FileName));
                 if (Directory.Exists(fileDir))
                 {
                     Directory.Delete(fileDir, true);
                     Directory.CreateDirectory(fileDir);
                 }
                 else Directory.CreateDirectory(fileDir);

                 var filePath = Path.Combine(fileDir, file.FileName);

                 using FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                 file.CopyTo(fs);
                 fs.Flush();
                 fs.Close();

                 if (Regex.IsMatch(file.FileName, "(.*?).zip"))
                 {
                     ZipFile.ExtractToDirectory(filePath, fileDir);
                     File.Delete(filePath);
                 }

                 List<PluginInfo> Info = new List<PluginInfo>();


                 Directory.GetFiles(fileDir).ForEnumerEach(item =>
                 {
                     var path = Path.Combine(fileDir, item);

                     using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                     RuntimeLoadContext context = new RuntimeLoadContext();
                     var Ass = context.LoadFromStream(stream);
                     if (Ass.GetTypes().Where(t => t.GetInterface(nameof(IPluginContext)) != null).Count() > 0)
                     {
                         Info.Add(new PluginInfo
                         {
                             PluginName = Ass.ManifestModule.ScopeName,
                             PluginSize = Math.Ceiling(new FileInfo(path).Length * 1.0 / 1024) + "KB",
                             PluginRoute = Path.GetFileNameWithoutExtension(file.FileName),
                         });
                     }
                     stream.Flush();
                     stream.Close();
                     context.Unload();
                 });

                 return Info;
             }, ex => throw ex);
        }
    }
}
