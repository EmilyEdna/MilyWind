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
        /// <summary>
        /// 调用插件 例如: PluginLoad.Excute("lib1", "libtest1", "test1", new object[] { "张三"});
        /// </summary>
        /// <param name="dllName"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object Excute(string dllName, string className, string methodName, params object[] param)
        {
            PluginInfo Info = Caches.MongoDBCacheGet<PluginInfo>(t => t.PluginName.ToLower() == $"{dllName}.dll".ToLower() && t.IsEable == true);
            //using (var stream = new MemoryStream(Info.Files))
            //{
            //    //PluginLoadContext context = new PluginLoadContext();
            //    //var assembly = context.LoadFromStream(stream);
            //    //var type = assembly.GetTypes().FirstOrDefault(t => t.Name.ToLower().Equals(className.ToLower()));
            //    //var result = type.GetMethods().FirstOrDefault(t => t.Name.ToLower() == methodName).Invoke(Activator.CreateInstance(type), param);
            //    //context.Unload();
            //    //return result;
            //    return null;
            //}
            return null;
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
                 if (!Directory.Exists(fileDir)) Directory.CreateDirectory(fileDir);
                 if (Directory.GetFiles(fileDir).Count() > 0)
                 {
                     Directory.Delete(fileDir, true);
                     Directory.CreateDirectory(fileDir);
                 }

                 var filePath = Path.Combine(fileDir, file.FileName);

                 if (File.Exists(filePath)) File.Delete(filePath);
                 using FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite);
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
                     PluginLoadContext context = new PluginLoadContext(path);
                     var Ass = context.LoadFromAssemblyPath(path);
                     if (Ass.GetTypes().Where(t => t.GetInterface(nameof(IPlugin)) != null).Count() > 0) 
                     {
                         Info.Add(new PluginInfo
                         {
                              PluginName = Ass.GetName().Name,
                              PluginSize= Math.Ceiling(item.Length * 1.0 / 1024) + "KB",
                              PluginRoute = Path.GetFileNameWithoutExtension(file.FileName),
                         });
                     }
                     context.Unload();
                 });

                 return Info;
             }, ex => throw ex);
        }
        /// <summary>
        /// 删除插件
        /// </summary>
        /// <param name="PluginId"></param>
        /// <returns></returns>
        public static Task RemovePlugin(Guid PluginId)
        {
            Caches.MongoDBCacheRemove<PluginInfo>(t => t.Id == PluginId);
            var Id = new string[] { PluginId.ToString() };
            MongoDbCaches.Instance.GetCollection<PluginMethodInfo>(nameof(PluginMethodInfo)).DeleteMany(Builders<PluginMethodInfo>.Filter.In(t => t.PluginId, Id));
            MongoDbCaches.Instance.GetCollection<PluginClassInfo>(nameof(PluginClassInfo)).DeleteMany(Builders<PluginClassInfo>.Filter.In(t => t.PluginId, Id));
            MongoDbCaches.Instance.GetCollection<PluginGroupExcuteInfo>(nameof(PluginGroupExcuteInfo)).DeleteMany(Builders<PluginGroupExcuteInfo>.Filter.In(t => t.PluginId, Id));
            return Task.CompletedTask;
        }
    }
}
