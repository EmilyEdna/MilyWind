using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mily.Wind.VMod;
using Mily.Wind.VMod.DataTransferObj;
using Mily.Wind.VMod.Enums;
using Newtonsoft.Json;

namespace Mily.Wind.Extens.SystemConfig
{
    public class MilyMapperResult
    {
        public string Code { get; set; }
        public object Result { get; set; }
        [JsonIgnore]
        public Type MapTo { get; set; }
        [JsonIgnore]
        public Type MapsTo { get; set; }
        [JsonIgnore]
        public Type Source { get; set; }
        [JsonIgnore]
        public MapperEnum MapType { get; set; }
        public static MilyMapperResult Instance(Action<MilyMapperResult> action)
        {
            MilyMapperResult result = new MilyMapperResult();
            action(result);
            if (result.Code != DSConst.DS002 && result.MapType == MapperEnum.Collection && result.MapsTo == null)
                throw new ArgumentNullException($"{nameof(MapsTo)} can't be null");
            return result;
        }

        public static MilyMapperResult Success<T, K>(MapperEnum mapper, object data)
        {
            return Instance(t =>
             {
                 t.Code = DSConst.DS001;
                 t.MapTo = typeof(K);
                 t.MapsTo = typeof(List<K>);
                 t.Source = typeof(T);
                 t.MapType = mapper;
                 t.Result = data;
             });
        }

        public static MilyMapperResult Error()
        {
            return Instance(t =>
            {
                t.Code = DSConst.DS002;
                t.Result = new DefaultVM();
            });
        }

        public static MilyMapperResult DefaultSuccess(object data)
        {
            return Instance(t =>
            {
                t.MapType = MapperEnum.DefaultSuccess;
                t.Code = DSConst.DS001;
                t.Result = data;
            });
        }
    }

}