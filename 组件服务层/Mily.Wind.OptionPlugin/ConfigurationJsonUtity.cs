using Mily.Wind.OptionPlugin.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;
using XExten.Advance.StaticFramework;

namespace Mily.Wind.OptionPlugin
{
    internal class ConfigurationJsonUtity
    {
        private static ConfigurationOption Option;
        static ConfigurationJsonUtity()
        {
            Option = ConfigurationIoc.GetService<ConfigurationOption>(nameof(ConfigurationOption));
        }

        internal static Dictionary<string, string> GetConfig()
        {
            return SyncStatic.TryCatch(() =>
            {
                List<KeyValue> kv = new List<KeyValue>();
                var data = IHttpMultiClient.HttpMulti.AddNode(node =>
                 {
                     node.ReqType = MultiType.POST;
                     node.NodePath = Option.Url + ConfigurationRoute.GetOption;
                     node.JsonParam = (new { Option.NameSpace, Option.Env }).ToJson();
                 }).Build().RunString().FirstOrDefault();

                data.ToModel<List<JObject>>().ForEach(item =>
                {
                    kv.AddRange(JsonXml(item.ToJson(), new List<KeyValue>()));
                });
                return kv.ToDictionary(t => t.Key, t => t.Value);
            }, ex => ReadFile<Dictionary<string, string>>());
        }

        internal static string GetVersion()
        {
            return SyncStatic.TryCatch(() =>
             {
                 return IHttpMultiClient.HttpMulti.AddNode(node =>
                 {
                     node.ReqType = MultiType.POST;
                     node.NodePath = Option.Url + ConfigurationRoute.GetVersion;
                     node.JsonParam = (new { Option.NameSpace, Option.Env }).ToJson();
                 }).Build().RunString().FirstOrDefault();
             }, ex => "-1");
        }

        private static string LocalConfig()
        {
            var route = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"application.{Option.NameSpace}.json");
            if (!File.Exists(route))
                File.Create(route).Dispose();
            return route;
        }

        private static T ReadFile<T>() where T : new()
        {
            var data = File.ReadAllText(LocalConfig());
            if (string.IsNullOrEmpty(data))
                return new T();
            return data.ToModel<T>();
        }

        internal static void WriteFile<T>(T args)
        {
            var res = args.ToJson();
            File.WriteAllText(LocalConfig(), res);
        }


        private static List<KeyValue> JsonXml(string json, List<KeyValue> kv)
        {
            json = json.Replace("\r\n", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
            var obj = json.ToModel<JObject>();
            foreach (var item in obj)
            {
                var target = item.Value.GetType();
                if (target == typeof(JObject) || target == typeof(JArray))
                    JsonXml(item.Value.ToString(), kv);
                else
                {
                    KeyValue md = new KeyValue();
                    md.Key = item.Key;
                    md.Value = item.Value.ToString();
                    kv.Add(md);
                }
            }
            return kv;
        }
    }
}
