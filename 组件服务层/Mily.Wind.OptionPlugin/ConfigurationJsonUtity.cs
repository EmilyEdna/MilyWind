using Mily.Wind.OptionPlugin.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NMyVision;
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

                DataDictionary DataDic = new DataDictionary();

                data.ToModel<List<JObject>>().ForEach(item =>
                {

                    DataDic.AddRange(DataDictionary.ParseJson(item.ToJson()));
                });

                DataDic.Flatten().ForDicEach((Key, Value) =>
                 {
                     kv.Add(new KeyValue
                     {
                         Key = Key.Replace(".",":"),
                         Value = Value.ToString()
                     });
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
            return DataDictionary.ParseJson(data).Flatten().ToJson().Replace(".",":").ToModel<T>();
        }

        internal static void WriteFile(IDictionary<string,string> args)
        {
            File.WriteAllText(LocalConfig(), ToFlattenJson(args));
        }

        /// <summary>
        /// 复杂Json还原成配置文件样式
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        internal static string ToFlattenJson(IDictionary<string, string> dict)
        {
            Dictionary<string, object> root = new Dictionary<string, object>();
            foreach (var kv in dict)
            {
                Generate(kv.Key, kv.Value, root);
            }

            return DictToJsonString(root);
        }

        private static string DictToJsonString(Dictionary<string, object> dict)
        {
          return  dict.ToJson(new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        /// <summary>
        /// 把扁平化的键值对还原成字典嵌套字典的模式
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="parent"></param>
        private static void Generate(string key, string value, Dictionary<string, object> parent)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var groupArr = key.Split(':');
            if (groupArr.Length > 1)
            {
                var sonKey = groupArr[0];
                var newArr = new string[groupArr.Length - 1];
                for (int i = 1; i < groupArr.Length; i++)
                {
                    newArr[i - 1] = groupArr[i];
                }
                var otherKeys = string.Join(':', newArr);
                if (parent.ContainsKey(sonKey))
                {
                    //如果已经有子字典
                    var son = parent[sonKey] as Dictionary<string, object>;
                    if (son != null)
                    {
                        Generate(otherKeys, value, son);
                    }
                }
                else
                {
                    var son = new Dictionary<string, object>();
                    Generate(otherKeys, value, son);
                    parent.Add(sonKey, son);
                }

            }
            else
            {
                parent.Add(key, value);
            }

        }
    }
}
