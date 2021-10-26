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
            foreach (var item in files)
            {
                var PluginInfos = PluginLoad.RegistPlugin(item);
                List<PluginInfo> CopyInfo = new List<PluginInfo>(PluginInfos);
                var PluginNames = PluginInfos.Select(x => x.PluginName).ToList();

                MongoDbCaches.SearchMany<PluginInfo>(t => PluginNames.Contains(t.PluginName)).ForEnumerEach(item =>
                {
                    var Flg = PluginInfos.FirstOrDefault(t => t.PluginName == item.PluginName);
                    if (Flg != null)
                    {
                        item.PluginRoute = Flg.PluginRoute;
                        item.PluginSize = Flg.PluginSize;
                        item.PluginVersion += 1;
                        item.RegistTime = DateTime.Now;
                        MongoDbCaches.UpdateMany(t => t.Id == item.Id, item);
                        CopyInfo.RemoveAll(t => t.PluginName == item.PluginName);
                    }
                });
                if (CopyInfo.Count > 0)
                    MongoDbCaches.InsertMany(CopyInfo);
            }
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
            var detail = query.OrderByDescending(t => t.RegistTime).Skip((input.PageIndex-1) * input.PageSize).Take(input.PageSize).ToList();
            return MilyMapperResult.Success<PluginOutput>(new PluginOutput
            {
                Detail = detail.ToMapest<List<PluginMogoViewModel>>(),
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
    }
}
