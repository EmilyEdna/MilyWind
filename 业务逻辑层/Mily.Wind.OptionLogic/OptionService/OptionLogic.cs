using Mily.Wind.VMod.Mogo;
using Mily.Wind.VMod.Mogo.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XExten.Advance.CacheFramework;
using XExten.Advance.CacheFramework.MongoDbCache;
using XExten.Advance.LinqFramework;

namespace Mily.Wind.OptionLogic.OptionService
{
    public class OptionLogic : IOptionLogic
    {
        public virtual OptionConfMogoViewModel WriteOptionConf(OptionConfInput input)
        {
            var vm = input.ToMapest<OptionConfMogoViewModel>();
            var res = Caches.MongoDBCacheGet<OptionConfMogoViewModel>(t => t.NameSpace == input.NameSpace);
            if (res != null) return null;
            vm.Version = 1;
            vm.Id = Guid.NewGuid();
            Caches.MongoDBCacheSet(vm);
            return vm;
        }
        public virtual Dictionary<string,List<OptionConfMogoViewModel>> SearchOptionConf()
        {
            List<OptionConfMogoViewModel> query = MongoDbCaches.Query<OptionConfMogoViewModel>().ToList();
           return query.GroupBy(t => t.Env).ToDictionary(t => t.Key.ToString(), t => t.ToList());
        }
        public virtual OptionConfMogoViewModel AlterOptionConf(OptionConfInput input)
        {
            OptionConfMogoViewModel vm = Caches.MongoDBCacheGet<OptionConfMogoViewModel>(t => t.Id == input.Id);
            vm.NameSpace = input.NameSpace;
            vm.OptionJson = input.OptionJson;
            vm.Version += 1;
            vm.Env = input.Env;
            MongoDbCaches.UpdateMany<OptionConfMogoViewModel>(t => t.Id == input.Id, vm);
            return vm;
        }
    }
}
