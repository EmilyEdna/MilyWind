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

        public static MilyMapperResult Instance(Action<MilyMapperResult> action)
        {
            MilyMapperResult result = new MilyMapperResult();
            action(result);
            return result;
        }

        public static MilyMapperResult Success<T>(object data)
        {
            return Instance(t =>
             {
                 t.Code = DSConst.DS001;
                 t.MapTo = typeof(T);
                 t.Result = data;
             });
        }

        public static MilyMapperResult DefaultSuccess(object data)
        {
            return Instance(t =>
            {
                t.Code = DSConst.DS001;
                t.Result = data;
            });
        }

        public static MilyMapperResult DefaultError()
        {
            return Instance(t =>
            {
                t.Code = DSConst.DS002;
                t.Result = new DefaultVM();
            });
        }
    }

}