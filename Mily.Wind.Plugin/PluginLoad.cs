using Microsoft.AspNetCore.Http;
using Mily.Wind.Plugin.Infos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.CacheFramework.MongoDbCache;
using XExten.Advance.LinqFramework;

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
        public static object Excute(string dllName, string className,string methodName, params object[] param)
        {
            PluginInfo Info = Caches.MongoDBCacheGet<PluginInfo>(t => t.PluginName.ToLower() == $"{dllName}.dll".ToLower() && t.IsEable == true);
            using (var stream = new MemoryStream(Info.Files))
            {
                PluginLoadContext context = new PluginLoadContext();
                var assembly = context.LoadFromStream(stream);
                var type = assembly.GetTypes().FirstOrDefault(t => t.Name.ToLower().Equals(className.ToLower()));
                var result = type.GetMethods().FirstOrDefault(t => t.Name.ToLower() == methodName).Invoke(Activator.CreateInstance(type), param);
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
            fs.Flush();
            var rtSteam = file.OpenReadStream();
            byte[] buffer = new byte[rtSteam.Length];
            rtSteam.Read(buffer, 0, buffer.Length);
            rtSteam.Seek(0, SeekOrigin.Begin);
            return buffer;
        }
        /// <summary>
        /// 注册类和方法名称
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="PluginId"></param>
        /// <returns></returns>
        public static Task RegistClassAndMethod(byte[] buffer, string PluginId)
        {
            using var stream = new MemoryStream(buffer);
            PluginLoadContext context = new PluginLoadContext();
            var assembly = context.LoadFromStream(stream);
            List<PluginClassInfo> ClassInfos = new List<PluginClassInfo>();
            List<PluginMethodInfo> MethodInfos = new List<PluginMethodInfo>();
            string[] Mehtonds = { "GetType", "ToString", "Equals", "GetHashCode" };
            assembly.GetTypes().ForEnumerEach(item =>
            {
                var ClassInfo = new PluginClassInfo
                {
                    ClassDescription = item.GetCustomAttribute<DescriptionAttribute>()?.Description,
                    Id = Guid.NewGuid(),
                    ClassName = item.Name,
                    PluginId = PluginId
                };
                ClassInfos.Add(ClassInfo);
                item.GetMethods().Where(t => !Mehtonds.Contains(t.Name)).ForEnumerEach(items =>
                {
                    var MethodInfo = new PluginMethodInfo
                    {
                        MethodDescription = items.GetCustomAttribute<DescriptionAttribute>()?.Description,
                        MethodName = items.Name,
                        PluginClassId = ClassInfo.Id.ToString(),
                        PluginId = PluginId
                    };
                    MethodInfos.Add(MethodInfo);
                });
            });
            var Id = new string[] { PluginId };
            MongoDbCaches.Instance.GetCollection<PluginMethodInfo>(nameof(PluginMethodInfo)).DeleteMany(Builders<PluginMethodInfo>.Filter.In(t=>t.PluginId, Id));
            MongoDbCaches.Instance.GetCollection<PluginClassInfo>(nameof(PluginClassInfo)).DeleteMany(Builders<PluginClassInfo>.Filter.In(t => t.PluginId, Id));
            MongoDbCaches.InsertMany(ClassInfos);
            MongoDbCaches.InsertMany(MethodInfos);
            return Task.CompletedTask;
        }
    }
}
