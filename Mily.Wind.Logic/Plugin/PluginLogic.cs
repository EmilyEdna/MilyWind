using Microsoft.AspNetCore.Http;
using Mily.Wind.Extens.AOPUtity;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.Plugin;
using Mily.Wind.Plugin.Infos;
using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
using Mily.Wind.VMod.Mogo.Output;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.CacheFramework.MongoDbCache;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.Logic.Plugin
{
    [Interceptor]
    public class PluginLogic : IPluginLogic
    {
        [Actions]
        public MilyMapperResult UploadPlugin(List<IFormFile> files)
        {
            List<PluginInfo> pluginInfos = new List<PluginInfo>();
            foreach (var item in files)
            {
                var buffer = PluginLoad.RegistPlugin(item);
                PluginInfo plugin = Caches.MongoDBCacheGet<PluginInfo>(t => t.PluginName == item.FileName);
                if (plugin != null)
                {
                    plugin.PluginVersion += 1;
                    plugin.PluginSize = Math.Ceiling(item.Length * 1.0 / 1024) + "KB";
                    plugin.Files = buffer;
                    PluginLoad.RegistClassAndMethod(buffer, plugin.Id.ToString());
                    MongoDbCaches.UpdateMany(t => t.Id == plugin.Id, plugin);
                }
                else
                {
                    var info = new PluginInfo
                    {
                        Id = Guid.NewGuid(),
                        PluginSize = Math.Ceiling(item.Length * 1.0 / 1024) + "KB",
                        PluginName = item.FileName,
                        Files = buffer
                    };
                    PluginLoad.RegistClassAndMethod(buffer, info.Id.ToString());
                    pluginInfos.Add(info);
                }
            }
            if (pluginInfos.Count > 0)
                MongoDbCaches.InsertMany(pluginInfos);
            return MilyMapperResult.DefaultSuccess(true);
        }
        [Actions]
        public MilyMapperResult GetPluginPage(PluginInput input)
        {
            var query = MongoDbCaches.Query<PluginInfo>().AsQueryable();
            if (input.IsEable.HasValue)
                query = query.Where(t => t.IsEable == input.IsEable);
            if (!input.PluginAlias.IsNullOrEmpty())
                query = query.Where(t => t.PluginAlias == input.PluginAlias);
            var detail = query.OrderByDescending(t => t.RegistTime).Skip(input.PageIndex * input.PageSize).Take(input.PageSize).ToList();
            return MilyMapperResult.Success<PluginOutput>(new PluginOutput
            {
                Detail = detail.ToMapest<List<PluginMapperInfo>>(),
                Total = query.Count()
            });
        }
        [Actions]
        public MilyMapperResult AlterPlugin(PluginAlterInput input)
        {
            if (!input.Type.HasValue)
                Caches.MongoDbCacheUpdate<PluginInfo>(t => t.Id == input.Id, nameof(input.PluginAlias), input.PluginAlias);
            else
            {
                if (input.Type.Value == 1)
                    Caches.MongoDbCacheUpdate<PluginInfo>(t => t.Id == input.Id, "IsEable", "true");
                else if (input.Type.Value == 0)
                    Caches.MongoDbCacheUpdate<PluginInfo>(t => t.Id == input.Id, "IsEable", "false");
                else
                    Caches.MongoDBCacheRemove<PluginInfo>(t => t.Id == input.Id);
            }
            return MilyMapperResult.DefaultSuccess(true);
        }
        [Actions]
        public MilyMapperResult GetPluginClassList(string input)
        {

            var data = MongoDbCaches.SearchMany<PluginClassInfo>(t => t.PluginId == input).ToMapest<List<PluginClassMapperInfo>>();
            return MilyMapperResult.Success<PluginClassInfoOutput>(new PluginClassInfoOutput
            {
                Detail = data
            });
        }
        [Actions]
        public MilyMapperResult GetPluginMethodList(string input)
        {
            var data = MongoDbCaches.SearchMany<PluginMethodInfo>(t => t.PluginId == input).ToMapest<List<PluginMethodMapperInfo>>();
            return MilyMapperResult.Success<PluginMethodInfoOutput>(new PluginMethodInfoOutput
            {
                Detail = data
            });
        }
    }
}
